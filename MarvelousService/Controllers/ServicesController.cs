using AutoMapper;
using CRM.APILayer.Attribites;
using Marvelous.Contracts.Enums;
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
    [AuthorizeRole(Role.Admin)]
    public class ServicesController : Controller
    {
        private readonly IServiceToService _serviceService;
        private readonly IMapper _autoMapper;
        private readonly ILogger<ServicesController> _logger;

        public ServicesController(IServiceToService serviceService, IMapper autoMapper, ILogger<ServicesController> logger)
        {
            _serviceService = serviceService;
            _autoMapper = autoMapper;
            _logger = logger;
        }

        //api/services
        [HttpPost]
        [SwaggerOperation("Add new service")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> AddService([FromBody] ServiceInsertRequest serviceInsertRequest)
        {
            _logger.LogInformation($"Получен запрос на добавление новой услуги.");
            var serviceModel = _autoMapper.Map<ServiceModel>(serviceInsertRequest);
            var id = await _serviceService.AddService(serviceModel);
            _logger.LogInformation($"Услуга с id = {id} успешно добавлена.");
            return StatusCode(StatusCodes.Status201Created, id);
        }

        //api/services/
        [HttpGet("id")]
        [SwaggerOperation("Get service by id")]
        [AuthorizeRole(Role.Admin)]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful", typeof(ServiceResponse))]
        public async Task<ActionResult<ServiceResponse>> GetServiceById(int id)
        {
            _logger.LogInformation($"Запрос на получение услуги по id = {id}");
            var serviceModel = await _serviceService.GetServiceById(id);
            var result = _autoMapper.Map<ServiceResponse>(serviceModel);
            _logger.LogInformation($"Услуга по id = {id} получена");
            return Ok(result);
        }

        //api/services/
        [HttpPut("id")]
        [SwaggerOperation("Update services")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionOutputModel), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ExceptionOutputModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ExceptionOutputModel), StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<ServiceUpdateRequest>> UpdateService(int id, ServiceUpdateRequest serviceUpdateRequest)
        {
            ServiceModel service = _autoMapper.Map<ServiceModel>(serviceUpdateRequest);
            await _serviceService.UpdateService(id, service);
            return Ok(service);
        }

        //api/services/
        [HttpPatch("id")]
        [SwaggerOperation("Deleted services")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionOutputModel), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ExceptionOutputModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ExceptionOutputModel), StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<ServiceDeletedRequest>> SoftDelete(int id, ServiceDeletedRequest serviceDeletedRequest)
        {
            ServiceModel service = _autoMapper.Map<ServiceModel>(serviceDeletedRequest);
            await _serviceService.SoftDelete(id, service);
            return Ok(service);
        }
    }
}
