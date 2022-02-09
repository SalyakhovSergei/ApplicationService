using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using Scoring.Service.Repository;

namespace Scoring.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScoringController : ControllerBase
    {
        private IScoringRepository _scoringRepository;
        readonly Logger logger = LogManager.GetCurrentClassLogger();


        public ScoringController(IScoringRepository scoringRepository)
        {
            _scoringRepository = scoringRepository;
        }

        [HttpPost]
        [Route("evaluate")]
        public async Task<IActionResult> Evaluate() => 
            await Task.Run(() =>
            {
                var response = _scoringRepository.EvaluateAsync();

                return Ok(response.Result);
            });
    }
}
