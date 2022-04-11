using AutoMapper;
using Marvelous.Contracts.Enums;
using MarvelousService.API.Models;
using MarvelousService.BusinessLayer.Helpers;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Clients.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MarvelousService.API.Controllers
{
    [ApiController]
    [Route("api/leadResources")]
    public class LeadResourcesController : ControllerExtensions
    {
        private readonly ILeadResourceService _leadResourceService;
        private readonly IResourceService _resourceService;
        private readonly IMapper _autoMapper;
        private readonly ILogger<ResourcesController> _logger;
        private readonly IRequestHelper _requestHelper;

        public LeadResourcesController(
            IMapper autoMapper,
            ILeadResourceService leadResource,
            IResourceService resourceService,
            IRequestHelper requestHelper,
            ILogger<ResourcesController> logger) : base(requestHelper, logger)
        {
            _leadResourceService = leadResource;
            _resourceService = resourceService;
            _autoMapper = autoMapper;
            _requestHelper = requestHelper;
            _logger = logger;
        }

        //api/leadResources
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [SwaggerOperation("Add a resource to a lead")]
        public async Task<ActionResult<int>> AddLeadResource([FromBody] LeadResourceInsertRequest leadResourceInsertRequest)
        {
            var lead = await CheckRole(Role.Regular, Role.Vip); 
            _logger.LogInformation($"Access to the method for lead {lead} granted");
            _logger.LogInformation($"Request for adding a Resource {leadResourceInsertRequest.ResourceId} to Lead {lead}.");
            var leadResourceModel = _autoMapper.Map<LeadResourceModel>(leadResourceInsertRequest);
            var resource = _resourceService.GetResourceById(leadResourceInsertRequest.ResourceId);
            leadResourceModel.Resource = resource.Result;
            var id = await _leadResourceService.AddLeadResource(leadResourceModel, Role.Vip);
            var id = await _leadResourceService.AddLeadResource(leadResourceModel, 2);
            return StatusCode(StatusCodes.Status201Created, id);
        }

        //api/leadResources
        [HttpGet("id")]
        [SwaggerOperation("Get lead resources by id")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful", typeof(List<LeadResourceResponse>))]
        public async Task<ActionResult<List<LeadResourceResponse>>> GetLeadResourcesById(int id)
        {
            _logger.LogInformation($"Request for getting all lead resources with id {id}");
            var leadResourceModelList = await _leadResourceService.GetById(id);
            var result = _autoMapper.Map<List<LeadResourceResponse>>(leadResourceModelList);
            _logger.LogInformation($"Lead resources were received by id {id}");
            return Ok(result);
        }

        //api/leadResources
        [HttpGet("payDate")]
        [SwaggerOperation("Get lead resources by pay date")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful", typeof(List<LeadResourceByPayDateResponse>))]
        public async Task<ActionResult<List<LeadResourceResponse>>> GetLeadResourcesByPayDate([FromQuery] DateTime payDate)
        {
            _logger.LogInformation($"Request for getting all lead resources with pay date {payDate}");
            var leadResourceModelList = await _leadResourceService.GetByPayDate(payDate);
            var result = _autoMapper.Map<List<LeadResourceByPayDateResponse>>(leadResourceModelList);
            _logger.LogInformation($"Lead resources were received by pay date {payDate}");
            return Ok(result);
        }
    }
}
