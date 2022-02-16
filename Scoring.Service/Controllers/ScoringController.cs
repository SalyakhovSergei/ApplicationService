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
        readonly Logger logger = LogManager.GetCurrentClassLogger();


        public ScoringController(IScoringRepository scoringRepository, IPublisher publisher)
        {
            _scoringRepository = scoringRepository;
            _publisher = publisher;
        }

        [HttpPost]
        [Route("evaluate")]
        public async Task<IActionResult> Evaluate() => 
            await Task.Run(() =>
            {
                
                var response = _scoringRepository.EvaluateAsync();
                _publisher.PublishToQueue(response.Result);

                return Ok(response.Result);
            });
    }
}
