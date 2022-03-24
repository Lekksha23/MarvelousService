using AutoMapper;
using CRM.APILayer.Attribites;
using Marvelous.Contracts;
using MarvelousService.API.Extensions;
using MarvelousService.API.Models;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MarvelousService.API.Controllers
{
    [ApiController]
    [Route("api/servicesToLead")]
    public class ServiceToLeadsController : Controller
    {
        private readonly IServiceToService _serviceService;
        private readonly IServiceToLeadService _serviceToLeadService;
        private readonly IMapper _autoMapper;
        private readonly ILogger<ServicesController> _logger;

        public ServiceToLeadsController(IServiceToService serviceService, IMapper autoMapper, IServiceToLeadService serviceToLead, ILogger<ServicesController> logger)
        {
            _serviceService = serviceService;
            _serviceToLeadService = serviceToLead;
            _autoMapper = autoMapper;
            _logger = logger;
        }

        //api/servicesToLead
        [HttpPost]
        [AuthorizeRole(Role.Vip, Role.Regular)]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [SwaggerOperation("Add service to a lead")]
        public async Task<ActionResult<int>> AddServiceToLead([FromBody] ServiceToLeadInsertRequest serviceToLeadInsertRequest)
        {
            var leadIdentity = this.GetLeadFromToken();
            _logger.LogInformation($"Получен запрос на оформление услуги лиду c id = {leadIdentity.Id}.");
            var serviceToLeadModel = _autoMapper.Map<ServiceToLeadModel>(serviceToLeadInsertRequest);
            serviceToLeadModel.LeadId = leadIdentity.Id;
            Role role = leadIdentity.Role;
            var id = await _serviceToLeadService.AddServiceToLead(serviceToLeadModel, (int)role);
            _logger.LogInformation($"Подписка/разовая услуга с id = {id} добавлена лиду с id = {serviceToLeadModel.LeadId}.");
            return StatusCode(StatusCodes.Status201Created, id);
        }

        //api/servicesToLead
        [HttpGet("id")]
        [AuthorizeRole(Role.Regular, Role.Vip)]
        [SwaggerOperation("Get servicesToLead by id")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful", typeof(List<ServiceToLeadResponse>))]
        public async Task<ActionResult<List<ServiceToLeadResponse>>> GetServiceToLeadById(int id)
        {
            _logger.LogInformation($"Запрос на получение всех услуг по id = {id}");

            var serviceToLeadModel = await _serviceToLeadService.GetServiceToLeadById(id);
            var result = _autoMapper.Map<List<ServiceToLeadResponse>>(serviceToLeadModel);

            _logger.LogInformation($"Услуги по id = {id} получены");

            return Ok(result);
        }

    }
}
