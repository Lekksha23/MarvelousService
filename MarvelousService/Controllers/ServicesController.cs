using AutoMapper;
using CRM.APILayer.Attribites;
using Marvelous.Contracts.Enums;
using MarvelousService.API.Extensions;
using MarvelousService.API.Models;
using MarvelousService.API.Models.ExceptionModel;
using MarvelousService.API.Models.Request;
using MarvelousService.API.Producer.Interface;
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
        private readonly IServiceProducer _serviceProducer;

        public ServicesController(IServiceToService serviceService, IMapper autoMapper, ILogger<ServicesController> logger, IServiceProducer serviceProducer)
        {
            _serviceService = serviceService;
            _autoMapper = autoMapper;
            _logger = logger;
            _serviceProducer = serviceProducer;
        }

        //api/services
        [HttpPost]
        [AuthorizeRole(Role.Admin)]
        [SwaggerOperation("Add new service")]       
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> AddService([FromBody] ServiceInsertRequest serviceInsertRequest)
        {
            var leadIdentity = this.GetLeadFromToken();

            _logger.LogInformation($"Received a request to add a new service.");

            var serviceModel = _autoMapper.Map<ServiceModel>(serviceInsertRequest);
            serviceModel.Id = leadIdentity.Id;
            Role role = leadIdentity.Role;
            var id = await _serviceService.AddService(serviceModel, (int)role);

            _logger.LogInformation($"Service with id = {id} added successfully.");

            await _serviceProducer.NotifyServiceAdded(id);

            return StatusCode(StatusCodes.Status201Created, id);
        }

        //api/services/
        [HttpGet("id")]
        [AuthorizeRole(Role.Admin)]
        [SwaggerOperation("Get service by id")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful", typeof(ServiceResponse))]
        public async Task<ActionResult<ServiceResponse>> GetServiceById(int id)
        {
            _logger.LogInformation($"Service request for id = {id}");
            var serviceModel = await _serviceService.GetServiceById(id);
            var result = _autoMapper.Map<ServiceResponse>(serviceModel);
            _logger.LogInformation($"Service by id = {id} received");
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
        public async Task<ActionResult<ServiceUpdateRequest>> UpdateService(int id, ServiceUpdateRequest serviceUpdateRequest)
        {
            var leadIdentity = this.GetLeadFromToken();

            _logger.LogInformation($"Poluchen zapros na obnovlenie service s id = {id}.");

            ServiceModel service = _autoMapper.Map<ServiceModel>(serviceUpdateRequest);

            var serviceId = leadIdentity.Id;
            service.Id = id;
            await _serviceService.UpdateService(serviceId, service);
            _logger.LogInformation($"Service c id = {id} uspeshno obnovlen.");
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
        public async Task<ActionResult<ServiceDeletedRequest>> SoftDelete(int id, ServiceDeletedRequest serviceDeletedRequest)
        {
            var leadIdentity = this.GetLeadFromToken();

            _logger.LogInformation($"Poluchen zapros na ydalenie service s id = {id}.");

            ServiceModel service = _autoMapper.Map<ServiceModel>(serviceDeletedRequest);

            var serviceId = leadIdentity.Id;
            service.Id = id;
            await _serviceService.SoftDelete(serviceId, service);
            _logger.LogInformation($"Service c id = {id} uspeshno ydalen.");
            return Ok(service);
        }
    }
}
