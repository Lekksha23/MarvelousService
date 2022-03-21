using AutoMapper;
using CRM.APILayer.Attribites;
using Marvelous.Contracts;
using MarvelousService.API.Models;
using MarvelousService.API.Models.ExceptionModel;
using MarvelousService.API.Models.Request;
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


        //api/services/
        [HttpGet("id")]
        [AuthorizeRole(Role.Admin)]
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

        //api/services/
        [HttpPut("id")]
        [AuthorizeRole(Role.Admin)]
        [SwaggerOperation("Update services")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionOutputModel), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ExceptionOutputModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ExceptionOutputModel), StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<ServiceUpdateRequest>> UpdateService(long id, ServiceUpdateRequest serviceUpdateRequest)
        {
            ServiceModel service = _autoMapper.Map<ServiceModel>(serviceUpdateRequest);

            await _serviceService.UpdateService(id, service);

            return Ok(service);
        }




        //api/services/
        [HttpPatch("id")]
        [AuthorizeRole(Role.Admin)]
        [SwaggerOperation("Deleted services")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionOutputModel), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ExceptionOutputModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ExceptionOutputModel), StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<ServiceDeletedRequest>> SoftDelete(long id, ServiceDeletedRequest serviceDeletedRequest)
        {
            ServiceModel service = _autoMapper.Map<ServiceModel>(serviceDeletedRequest);

            await _serviceService.SoftDelete(id, service);

            return Ok(service);
        }

    }

}
