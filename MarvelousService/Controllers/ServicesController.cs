using AutoMapper;
using CRM.APILayer.Attribites;
using Marvelous.Contracts;
using MarvelousService.API.Models;
using MarvelousService.API.Models.ExceptionModel;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MarvelousService.API.Controllers
{
    [ApiController]
    [Route("api/services")]
    public class ServicesController : Controller
    {
        private readonly IServiceToService _serviceService;
        private readonly IServiceToLeadService _serviceToLeadService;
        private readonly IMapper _autoMapper;
        private readonly ILogger<ServicesController> _logger;

        public ServicesController(IServiceToService serviceService, IMapper autoMapper, IServiceToLeadService serviceToLead, ILogger<ServicesController> logger)
        {
            _serviceService = serviceService;
            _serviceToLeadService = serviceToLead;
            _autoMapper = autoMapper;
            _logger = logger;
        }

        //api/services
        [HttpPost]
        [AuthorizeRole(Role.Admin)]
        [SwaggerOperation("Add new service")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ExceptionOutputModel), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ExceptionOutputModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<long>> AddService([FromBody] ServiceInsertRequest serviceInsertRequest)
        {
            _logger.LogInformation($"Получен запрос на добавление новой услуги.");
            var serviceModel = _autoMapper.Map<ServiceModel>(serviceInsertRequest);
            var id = await _serviceService.AddService(serviceModel);
            _logger.LogInformation($"Услуга с id = {id} успешно добавлена.");
            return StatusCode(StatusCodes.Status201Created, id);
        }

        //api/services
        [HttpPost("service")]
        [SwaggerOperation("Add service to lead")]
        [ProducesResponseType(typeof(ServiceToLeadResponse), StatusCodes.Status201Created)]
        public async Task<ActionResult<ServiceToLeadResponse>> AddServiceToLead([FromBody] ServiceToLeadInsertRequest serviceToLeadInsertRequest)
        {
            _logger.LogInformation($"Получен запрос на добавление услуги лиду с id = .");
            var serviceToLeadModel = _autoMapper.Map<ServiceToLeadModel>(serviceToLeadInsertRequest);
            var serviceToLead = await _serviceToLeadService.AddServiceToLead(serviceToLeadModel);
            _logger.LogInformation($"Услуга с id = {serviceToLeadModel.ServiceId} успешно добавлена лиду с id = .");
            return StatusCode(StatusCodes.Status201Created, serviceToLead);
        }

        //api/services/
        [HttpGet("services/{id}")]
        [SwaggerOperation("Get services by id")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful", typeof(List<ServiceResponse>))]
        public async Task<ActionResult<List<ServiceResponse>>> GetServiceById(long id)
        {
            _logger.LogInformation($"Запрос на получение всех услуг по id = {id}");

            var serviceModel = await _serviceService.GetServiceById(id);
            var result = _autoMapper.Map<List<ServiceResponse>>(serviceModel);

            _logger.LogInformation($"Услуги по id = {id} получены");

            return Ok(result);
        }
    }
}
