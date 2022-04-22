using AutoMapper;
using MarvelousService.API.Models;
using MarvelousService.API.Producer.Interface;
using MarvelousService.BusinessLayer.Helpers;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Clients.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using MarvelousService.API.Extensions;
using Marvelous.Contracts.Enums;
using FluentValidation;

namespace MarvelousService.API.Controllers
{
    [ApiController]
    [Route("api/resources")]
    [SwaggerTag("Access only for admins.")]
    public class ResourcesController : ControllerExtensions
    {
        private readonly IResourceService _resourceService;
        private readonly IMapper _autoMapper;
        private readonly ILogger<ResourcesController> _logger;
        private readonly IResourceProducer _resourceProducer;
        private readonly IRequestHelper _requestHelper;
        private readonly IValidator<ResourceInsertRequest> _validatorResourceInsertRequest;
        private readonly IValidator<ResourceSoftDeleteRequest> _validatorSoftDeleteReques;
        private readonly IValidator<ResourceUpdateRequest> _validatorUpdateRequest;


        public ResourcesController(            
            IResourceService resourceService, 
            IMapper autoMapper, 
            ILogger<ResourcesController> logger, 
            IResourceProducer resourceProducer,
            IRequestHelper requestHelper,
            IValidator<ResourceSoftDeleteRequest> validatorSoftDeleteReques,
            IValidator<ResourceInsertRequest> validatorResourceInsertRequest) : base(requestHelper, logger)            
        {
            _resourceService = resourceService;
            _autoMapper = autoMapper;
            _logger = logger;
            _resourceProducer = resourceProducer;
            _requestHelper = requestHelper;
            _validatorResourceInsertRequest = validatorResourceInsertRequest;
            _validatorSoftDeleteReques = validatorSoftDeleteReques;
        }

        //api/resources
        [HttpPost]
        [SwaggerOperation("Add a new resource")]       
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> AddResource([FromBody] ResourceInsertRequest resourceInsertRequest)
        {
            _logger.LogInformation($"Received a request to add a new resource.");
            var validationResult = await _validatorResourceInsertRequest.ValidateAsync(resourceInsertRequest);

            if(validationResult.IsValid)
            {
                var lead = await CheckRole(Role.Admin);
                _logger.LogInformation($"Role - {lead} successfully verified.");
                var resourceModel = _autoMapper.Map<ResourceModel>(resourceInsertRequest);
                var id = await _resourceService.AddResource(resourceModel);
                _logger.LogInformation($"Resource with id {id} successfully added.");
                await _resourceProducer.NotifyResourceAdded(resourceModel.Id);

                return StatusCode(StatusCodes.Status201Created, id);
            }
            else
            {
                _logger.LogError("Error: ResourceInsertRequest isn't valid");
                throw new ValidationException("ResourceInsertRequest isn't valid");
            }
        }

        //api/resources/
        [HttpGet("id")]
        [SwaggerOperation("Get resource by id")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful", typeof(ResourceResponse))]
        public async Task<ActionResult<ResourceResponse>> GetResourceById(int id)
        {
            _logger.LogInformation($"Request for getting a resource by id {id}");
            var lead = await CheckRole(Role.Admin);
            _logger.LogInformation($"Role - {lead.Role} successfully verified.");
            var resourceModel = await _resourceService.GetResourceById(id);
            var result = _autoMapper.Map<ResourceResponse>(resourceModel);
            _logger.LogInformation($"Resource by id {id} was received");
            await _resourceProducer.NotifyResourceAdded(id);
            return Ok(result);
        }

        //api/resources
        [HttpGet("getAll")]
        [SwaggerOperation("Get all resources")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful", typeof(ResourceResponse))]
        public async Task<ActionResult<ResourceResponse>> GetAllResources()
        {
            _logger.LogInformation($"Request for receiving all resources");
            var lead = await CheckRole(Role.Admin);
            _logger.LogInformation($"Role - {lead.Role} successfully verified.");
            var resourceModels = await _resourceService.GetAllResources();
            var result = _autoMapper.Map<List<ResourceResponse>>(resourceModels);
            _logger.LogInformation($"Resources received");
            return Ok(result);
        }

        //api/resources
        [HttpGet("getActive")]
        [SwaggerOperation("Get active resource")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful", typeof(ResourceResponse))]
        public async Task<ActionResult<ResourceResponse>> GetActiveResource()
        {
            _logger.LogInformation($"Request for receiving all resources");
            var lead = await CheckRole(Role.Admin);
            _logger.LogInformation($"Role - {lead.Role} successfully verified.");
            var resourceModels = await _resourceService.GetActiveResourceService();
            var result = _autoMapper.Map<List<ResourceResponse>>(resourceModels);
            _logger.LogInformation($"Resources received");
            return Ok(result);
        }

        //api/resources
        [HttpPut("id")]
        [SwaggerOperation("Update a resource")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<ResourceResponse>> UpdateResource(int id, ResourceUpdateRequest resourceUpdateRequest)
        {
            _logger.LogInformation($"Request for updating a resource with id {id}.");

            var validationResult = await _validatorResourceInsertRequest.ValidateAsync(resourceUpdateRequest);

            if(validationResult.IsValid)
            {
                var lead = await CheckRole(Role.Admin);
                _logger.LogInformation($"Role - {lead} successfully verified.");
                ResourceModel resource = _autoMapper.Map<ResourceModel>(resourceUpdateRequest);
                await _resourceService.UpdateResource(id, resource);
                _logger.LogInformation($"Resource with id {id} successfully received.");
                await _resourceProducer.NotifyResourceAdded(id);
                return Ok(resource);
            }
            else
            {
                _logger.LogError("Error: ResourceUpdateRequest isn't valid");
                throw new ValidationException("ResourceUpdatetRequest isn't valid");
            }
            
        }

        //api/resources
        [HttpDelete("id")]
        [SwaggerOperation("Delete a resource")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<ResourceResponse>> SoftDelete(int id, ResourceSoftDeleteRequest resourceDeleteRequest)
        {
            _logger.LogInformation($"Request for deletion a resource with id {id}.");

            var validationResult = await _validatorSoftDeleteReques.ValidateAsync(resourceDeleteRequest);

            if(validationResult.IsValid)
            {
                var lead = await CheckRole(Role.Admin);
                _logger.LogInformation($"Role - {lead} successfully verified.");
                ResourceModel service = _autoMapper.Map<ResourceModel>(resourceDeleteRequest);
                await _resourceService.SoftDelete(id, service);
                _logger.LogInformation($"Resource with id {id} successfully deleted.");
                await _resourceProducer.NotifyResourceAdded(id);
                return Ok(service);
            }
            else
            {
                _logger.LogError("Error: ResourceSoftDeleteRequest isn't valid");
                throw new ValidationException("ResourceSoftDeleteRequest isn't valid");
            }
        }
    }
}
