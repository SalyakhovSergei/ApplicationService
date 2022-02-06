using System.Threading.Tasks;
using Application.Data.DataObjects;
using Application.Data.RepositoryInterfaces;
using Application.Integration.ScoringService;
using Application.Service.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Application.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationController : ControllerBase
    {
        private IApplicationRepository _applicationRepository;
        private IScoringService _scoringService;
        private IMapper _mapper;

        public ApplicationController(IApplicationRepository applicationRepository, 
            IMapper mapper, 
            IScoringService scoringService)
        {
            _applicationRepository = applicationRepository;
            _mapper = mapper;
            _scoringService = scoringService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateApplication([FromBody] Models.Application application)
        {
            var app = _mapper.Map<ApplicationDTO>(application);
            await _applicationRepository.Create(app);
            return Ok();
        }

        [HttpGet]
        [Route("status/{appnumber}")]
        public async Task<IActionResult> GetRequestResponse(string appnumber)
        {
            
            var response = await _applicationRepository.GetRequestResponse(appnumber);
            return Ok(response);

        }
    }
}