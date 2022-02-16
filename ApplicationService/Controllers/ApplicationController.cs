using System;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Application.Data.DataObjects;
using Application.Data.RepositoryInterfaces;
using Application.Data.ResponseData;
using Application.Integration.ScoringService;
using Application.Service.Models;
using Application.Service.RabbitMQ;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Application.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationController : ControllerBase
    {
        private const int timeToWaitQueue = 10000;

        private IApplicationRepository _applicationRepository;
        private IPublisher _publisher;
        private IConsumer _consumer;

        private IMapper _mapper;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public ApplicationController(IApplicationRepository applicationRepository,
            IMapper mapper, 
            IPublisher publisher, 
            IConsumer consumer)
        {
            _applicationRepository = applicationRepository;
            _mapper = mapper;
            _publisher = publisher;
            _consumer = consumer;
        }

        //main method that accept info from external source and send query to scoring and put data in database
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateApplication([FromBody] ApplicationQuery applicationQuery)
        {
            try
            {
                _publisher.PublishToQueue(applicationQuery);
                
                var app = _mapper.Map<ApplicationDTO>(applicationQuery);

                await GetResponse(app);

                await _applicationRepository.Create(app);
                _logger.Info($"Заявка {app.ApplicationNum} принята");
            }
            catch (WebException e)
            {
                _logger.Error(e.Message);
                throw;
            }
            
            return Ok();
        }

        //returns answer about application
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
        private async Task GetResponse(ApplicationDTO applicationDto)
        {
            await Task.Run(() =>
            {
                try
                {
                    Thread.Sleep(timeToWaitQueue);
                    var answer = _consumer.GetMessageFromQueue();
                    var scoringResult = JsonConvert.DeserializeObject<ScoringResponse>(answer);
                    applicationDto.ScoringStatus = scoringResult?.ScoringStatus;
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