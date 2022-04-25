using AutoMapper;
using Marvelous.Contracts.Enums;
using MarvelousService.API.Models;
using MarvelousService.BusinessLayer.Helpers;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Clients.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using MarvelousService.API.Extensions;
using FluentValidation;
using MarvelousService.API.Producer.Interface;

namespace MarvelousService.API.Controllers
{
    [ApiController]
    [Route("api/leadResources")]
    public class LeadResourcesController : ControllerExtensions
    {
        private readonly ILeadResourceService _leadResourceService;
        private readonly IResourceService _resourceService;
        private readonly IMapper _autoMapper;
        private readonly ILogger<LeadResourcesController> _logger;
        private readonly IValidator<LeadResourceInsertRequest> _leadResourceInsertRequestValidator;
        private readonly IRequestHelper _requestHelper;
        private readonly IResourceProducer _resourceProducer;

        public LeadResourcesController(
            IMapper autoMapper,
            ILeadResourceService leadResource,
            IResourceService resourceService,
            IRequestHelper requestHelper,
            ILogger<LeadResourcesController> logger,
            IResourceProducer resourceProducer,
            IValidator<LeadResourceInsertRequest> leadResourceInsertRequestValidator) : base(requestHelper, logger)
        {
            _leadResourceService = leadResource;
            _resourceService = resourceService;
            _autoMapper = autoMapper;
            _requestHelper = requestHelper;
            _resourceProducer = resourceProducer;
            _logger = logger;
            _leadResourceInsertRequestValidator = leadResourceInsertRequestValidator;
        }

        //api/leadResources
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [SwaggerOperation("Add a resource to a lead. Roles: VIP, Regular")]
        public async Task<ActionResult<int>> AddLeadResource([FromBody] LeadResourceInsertRequest leadResourceInsertRequest)
        {
            var validationResult = await _leadResourceInsertRequestValidator.ValidateAsync(leadResourceInsertRequest);
            var lead = await CheckRole(Role.Regular, Role.Vip);
            if (validationResult.IsValid)
            {
                var role = (Role)Enum.Parse(typeof(Role), lead.Role);
                _logger.LogInformation($"Access to the method for lead {lead.Id} granted");
                _logger.LogInformation($"Request for adding a Resource {leadResourceInsertRequest.ResourceId} to Lead {lead.Id}.");
                var leadResourceModel = _autoMapper.Map<LeadResourceModel>(leadResourceInsertRequest);
                var resource = _resourceService.GetResourceById(leadResourceInsertRequest.ResourceId);
                leadResourceModel.Resource = resource.Result;
                leadResourceModel.LeadId = (int)lead.Id;
                var id = await _leadResourceService.AddLeadResource(leadResourceModel, role, HttpContext.Request.Headers.Authorization.First());
                return StatusCode(StatusCodes.Status201Created, id);
            }
            else
            {
                _logger.LogError("Error: LeadResourceInsertRequest isn't valid");
                throw new ValidationException("LeadResourceInsertRequest isn't valid");
            }
        }

        //api/leadResources
        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful", typeof(List<LeadResourceResponse>))]
        [SwaggerOperation("Get lead resource by id. Roles: VIP, Regular.")]
        public async Task<ActionResult<List<LeadResourceResponse>>> GetLeadResourceById(int id)
        {
            var lead = await CheckRole(Role.Vip, Role.Regular);
            var leadId = (int)lead.Id;
            _logger.LogInformation($"Request for getting lead resource with id {id}");
            var leadResourceModel = await _leadResourceService.GetById(id);
            var result = _autoMapper.Map<LeadResourceResponse>(leadResourceModel);
            _logger.LogInformation($"Lead resource was received by id {id}");
            return Ok(result);
        }

        //api/leadResources
        [HttpGet("leadId")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful", typeof(List<LeadResourceResponse>))]
        [SwaggerOperation("Get lead resources by LeadId. Roles: VIP, Regular.")]
        public async Task<ActionResult<List<LeadResourceResponse>>> GetLeadResourcesByLeadId()
        {
            var lead = await CheckRole(Role.Vip, Role.Regular);
            var leadId = (int)lead.Id;
            _logger.LogInformation($"Request for getting all lead resources with LeadId {leadId }");
            var leadResourceModelList = await _leadResourceService.GetByLeadId(leadId);
            var result = _autoMapper.Map<List<LeadResourceResponse>>(leadResourceModelList);
            _logger.LogInformation($"Lead resources were received by LeadId {leadId}");
            return Ok(result);
        }

        //api/leadResources
        [HttpGet("payDate")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful", typeof(List<LeadResourceByPayDateResponse>))]
        [SwaggerOperation("Get lead resources by pay date. Roles: VIP, Regular, Admin.")]
        public async Task<ActionResult<List<LeadResourceResponse>>> GetLeadResourcesByPayDate([FromQuery] DateTime payDate)
        {
            await CheckRole(Role.Vip, Role.Regular, Role.Admin);
            _logger.LogInformation($"Request for getting all lead resources with pay date {payDate}");
            var leadResourceModelList = await _leadResourceService.GetByPayDate(payDate);
            var result = _autoMapper.Map<List<LeadResourceByPayDateResponse>>(leadResourceModelList);
            _logger.LogInformation($"Lead resources were received by pay date {payDate}");
            return Ok(result);
        }
    }
}
