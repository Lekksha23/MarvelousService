using AutoMapper;
using CRM.APILayer.Attribites;
using Marvelous.Contracts;
using Marvelous.Contracts.Enums;
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
        private readonly IServiceToLeadService _serviceToLeadService;
        private readonly IMapper _autoMapper;
        private readonly ILogger<ServicesController> _logger;

        public ServiceToLeadsController(IMapper autoMapper, IServiceToLeadService serviceToLead, ILogger<ServicesController> logger)
        {
            _serviceToLeadService = serviceToLead;
            _autoMapper = autoMapper;
            _logger = logger;
        }

        //api/servicesToLead
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [SwaggerOperation("Add service to a lead")]
        public async Task<ActionResult<int>> AddServiceToLead([FromBody] ServiceToLeadInsertRequest serviceToLeadInsertRequest)
        {
            var leadIdentity = this.GetLeadFromToken();
            _logger.LogInformation($"A request for adding Service {serviceToLeadInsertRequest.ServiceId} to Lead {leadIdentity.Id} has been received.");
            var serviceToLeadModel = _autoMapper.Map<ServiceToLeadModel>(serviceToLeadInsertRequest);
            serviceToLeadModel.LeadId = leadIdentity.Id;
            Role role = leadIdentity.Role;
            var id = await _serviceToLeadService.AddServiceToLead(serviceToLeadModel, (int)role);
            _logger.LogInformation($"Subscription/one-time service with id = {id} added to lead with id = {serviceToLeadModel.LeadId}.");
            return StatusCode(StatusCodes.Status201Created, id);
        }

        //api/servicesToLead
        [HttpGet("id")]
        [AuthorizeRole(Role.Regular, Role.Vip)]
        [SwaggerOperation("Get servicesToLead by id")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful", typeof(List<ServiceToLeadResponse>))]
        public async Task<ActionResult<List<ServiceToLeadResponse>>> GetServiceToLeadById(int id)
        {
            var leadIdentity = this.GetLeadFromToken().Id;
            _logger.LogInformation($"Request for all services id = {id}");

            var serviceToLeadModel = await _serviceToLeadService.GetServiceToLeadById(leadIdentity);
            var result = _autoMapper.Map<List<ServiceToLeadResponse>>(serviceToLeadModel);

            _logger.LogInformation($"Services by id = {id} received");

            return Ok(result);
        }

    }
}
