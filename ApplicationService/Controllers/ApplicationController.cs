using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Data.DataObjects;
using Application.Data.RepositoryInterfaces;
using Application.Data.ResponseData;
using Application.Integration.ScoringService;
using Application.Service.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;

namespace Application.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationController : ControllerBase
    {
        private IApplicationRepository _applicationRepository;
        private IScoringService _scoringService;
        private IMapper _mapper;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

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
        public async Task<IActionResult> CreateApplication([FromBody] ApplicationQuery applicationQuery)
        {
            if (applicationQuery.Applicant == null)
            {
                return StatusCode(400, "Ошибка: Укажите клиента");
            }
            if (applicationQuery.RequestedCredit == null)
            {
                return StatusCode(400, "Ошибка: Нет информации о заявке на кредит");
            }
            try
            {
                var app = _mapper.Map<ApplicationDTO>(applicationQuery);

                GetResponse(app);
                await _applicationRepository.Create(app);
                _logger.Info($"Заявка {app.ApplicationNum} принята");
            }
            catch (WebException e)
            {
                _logger.Error(e.Message);
            }
            
            return Ok();
        }

        [HttpGet]
        [Route("status/{appnumber}")]
        public async Task<IActionResult> GetRequestResponse(string appnumber)
        {

            if (string.IsNullOrWhiteSpace(appnumber))
            {
                _logger.Error("Запрошена заявка с несуществующим номером");
                return StatusCode(400, $"Ошибка: Укажите номер заявки");
            }

            try
            {
                var response = await _applicationRepository.GetRequestResponse(appnumber);
                _logger.Info($"Запрошена информация по заявке {response.ApplicationNum}");
                return Ok(response);
            }
            catch (WebException exs)
            {
                _logger.Error(exs.Message);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            
            return Ok();
        }

        [HttpGet]
        private async void GetResponse(ApplicationDTO applicationDto)
        {
            await Task.Run(() =>
            {
                try
                {
                    var scoringResponse = _scoringService.Evaluate();
                    var scoringResult = JsonConvert.DeserializeObject<ScoringResponse>(scoringResponse);
                    applicationDto.ScoringStatus = scoringResult is { ScoringStatus: true };
                    applicationDto.ScoringDate = DateTime.Now;
                    _logger.Info("Получен ответ по заявке");
                }
                catch (WebException e)
                {
                    _logger.Error(e.Message);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message);
                }

            } );
        }
        
    }
}