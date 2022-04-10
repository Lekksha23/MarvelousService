using AutoMapper;
using CRM.APILayer.Attribites;
using Marvelous.Contracts.Enums;
using MarvelousService.API.Extensions;
using MarvelousService.API.Models;
using MarvelousService.BusinessLayer.Helpers;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Services.Interfaces;
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
        private readonly IReqvestHelper _reqvestHelper;

        public LeadResourcesController(
            IMapper autoMapper, 
            ILeadResourceService leadResource,
            IResourceService resourceService,
            ILogger<ResourcesController> logger, IReqvestHelper reqvestHelper) : base(reqvestHelper, logger)
        {
            _leadResourceService = leadResource;
            _resourceService = resourceService;
            _autoMapper = autoMapper;
            _logger = logger;
            _reqvestHelper = reqvestHelper;
        }

        //api/leadResources
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [SwaggerOperation("Add resource to a lead")]
        public async Task<ActionResult<int>> AddLeadResource([FromBody] LeadResourceInsertRequest leadResourceInsertRequest)
        {
            await this.CheckRole(Role.Regular,Role.Vip); // Плюнуть Ролью....

            _logger.LogInformation($"Request for adding a Resource {leadResourceInsertRequest.ResourceId} to Lead {leadIdentity.Id}.");
            var leadResourceModel = _autoMapper.Map<LeadResourceModel>(leadResourceInsertRequest);
            var resource = _resourceService.GetResourceById(leadResourceInsertRequest.ResourceId);
            leadResourceModel.Resource = resource.Result;
            leadResourceModel.LeadId = leadIdentity.Id;
            Role role = leadIdentity.Role;
            var id = await _leadResourceService.AddLeadResource(leadResourceModel, (int)role);
            return StatusCode(StatusCodes.Status201Created, id);
        }

        //api/leadResources
        [HttpGet("id")]
        [AuthorizeRole(Role.Regular, Role.Vip)]
        [SwaggerOperation("Get lead resources by id")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful", typeof(List<LeadResourceResponse>))]
        public async Task<ActionResult<List<LeadResourceResponse>>> GetLeadResourcesById(int id)
        {
            var leadIdentity = this.GetLeadFromToken().Id;
            _logger.LogInformation($"Request for getting all lead resources with id {id}");
            var leadResourceModelList = await _leadResourceService.GetById(leadIdentity);
            var result = _autoMapper.Map<List<LeadResourceResponse>>(leadResourceModelList);
            _logger.LogInformation($"Lead resources were received by id {id}");
            return Ok(result);
        }

    }
}
