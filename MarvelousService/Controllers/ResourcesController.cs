﻿using AutoMapper;
using MarvelousService.API.Models;
using MarvelousService.API.Producer.Interface;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MarvelousService.API.Controllers
{
    [ApiController]
    [Route("api/resources")]
    public class ResourcesController : Controller
    {
        private readonly IResourceService _resourceService;
        private readonly IMapper _autoMapper;
        private readonly ILogger<ResourcesController> _logger;
        private readonly IResourceProducer _resourceProducer;

        public ResourcesController(IResourceService resourceService, IMapper autoMapper, ILogger<ResourcesController> logger, IResourceProducer resourceProducer)
        {
            _resourceService = resourceService;
            _autoMapper = autoMapper;
            _logger = logger;
            _resourceProducer = resourceProducer;
        }

        //api/services
        [HttpPost]
        [SwaggerOperation("Add a new resource")]       
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> AddResource([FromBody] ResourceInsertRequest serviceInsertRequest)
        {
            _logger.LogInformation($"Received a request to add a new resource.");
            var resourceModel = _autoMapper.Map<ResourceModel>(serviceInsertRequest);
            var id = await _resourceService.AddResource(resourceModel);
            _logger.LogInformation($"Resource with id {id} successfully added.");
            await _resourceProducer.NotifyResourceAdded(resourceModel.Id);
            return StatusCode(StatusCodes.Status201Created, id);
        }

        //api/services/
        [HttpGet("id")]
        [SwaggerOperation("Get resource by id")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful", typeof(ResourceResponse))]
        public async Task<ActionResult<ResourceResponse>> GetResourceById(int id)
        {
            _logger.LogInformation($"Request for getting a resource by id {id}");
            var resourceModel = await _resourceService.GetResourceById(id);
            var result = _autoMapper.Map<ResourceResponse>(resourceModel);
            _logger.LogInformation($"Resource by id {id} was received");
            await _resourceProducer.NotifyResourceAdded(id);
            return Ok(result);
        }

        //api/services/
        [HttpGet()]
        [SwaggerOperation("Get all resources")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful", typeof(ResourceResponse))]
        public async Task<ActionResult<ResourceResponse>> GetAllResources()
        {
            _logger.LogInformation($"Request for receiving all resources");
            var resourceModels = await _resourceService.GetAllResources();
            var result = _autoMapper.Map<List<ResourceResponse>>(resourceModels);
            _logger.LogInformation($"Resources received");
            return Ok(result);
        }

        //api/services/
        [HttpGet("active id")]
        [SwaggerOperation("Get active resource")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful", typeof(ResourceResponse))]
        public async Task<ActionResult<ResourceResponse>> GetActiveResource()
        {
            _logger.LogInformation($"Request for receiving all resources");
            var resourceModels = await _resourceService.GetActiveResourceService();
            var result = _autoMapper.Map<List<ResourceResponse>>(resourceModels);
            _logger.LogInformation($"Resources received");
            return Ok(result);
        }

        //api/services/
        [HttpPut("id")]
        [SwaggerOperation("Update a resource")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<ResourceResponse>> UpdateResource(int id, ResourceUpdateRequest serviceUpdateRequest)
        {
            _logger.LogInformation($"Request for updating a resource with id {id}.");
            ResourceModel resource = _autoMapper.Map<ResourceModel>(serviceUpdateRequest);
            await _resourceService.UpdateResource(id, resource);
            _logger.LogInformation($"Resource with id {id} successfully received.");
            await _resourceProducer.NotifyResourceAdded(id);
            return Ok(resource);
        }

        //api/services/
        [HttpPatch("id")]
        [SwaggerOperation("Delete a resource")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<ResourceResponse>> SoftDelete(int id, ResourceSoftDeleteRequest serviceDeletedRequest)
        {
            _logger.LogInformation($"Request for deletion a resource with id {id}.");
            ResourceModel service = _autoMapper.Map<ResourceModel>(serviceDeletedRequest);
            await _resourceService.SoftDelete(id, service);
            _logger.LogInformation($"Resource with id {id} successfully deleted.");
            await _resourceProducer.NotifyResourceAdded(id);
            return Ok(service);
        }
    }
}
