using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using Scoring.Service.RabbitMQ;
using Scoring.Service.Repository;

namespace Scoring.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScoringController : ControllerBase
    {
        private IScoringRepository _scoringRepository;
        private IPublisher _publisher;
        private IConsumer _consumer;
        readonly Logger logger = LogManager.GetCurrentClassLogger();


        public ScoringController(IScoringRepository scoringRepository, IPublisher publisher, IConsumer consumer)
        {
            _scoringRepository = scoringRepository;
            _publisher = publisher;
            _consumer = consumer;
        }

        [HttpPost]
        [Route("evaluate")]
        public async Task<IActionResult> Evaluate() => 
            await Task.Run(() =>
            {
                _consumer.GetMessageFromQueue();
                var response = _scoringRepository.EvaluateAsync();
                Task.Delay(500);
                _publisher.PublishToQueue(response.Result);

                return Ok(response.Result);
            });
    }
}
