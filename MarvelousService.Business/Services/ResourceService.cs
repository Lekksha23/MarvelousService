using AutoMapper;
using MarvelousService.BusinessLayer.Clients.Interfaces;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using MarvelousService.BusinessLayer.Exceptions;

namespace MarvelousService.BusinessLayer.Clients
{
    public class ResourceService : IResourceService
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ResourceService> _logger;

        public ResourceService(IResourceRepository resourceRepository, IMapper mapper, ILogger<ResourceService> logger)
        {
            _resourceRepository = resourceRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> AddResource(ResourceModel resourceModel)
        {
            _logger.LogInformation("Request for adding a resource");
            _logger.LogInformation($"Request for getting a resource by id {resourceModel.Id}");
            var oldService =  _resourceRepository.GetResourceById(resourceModel.Id);
            CheckErrorHelper.CheckIfEntityIsNotNull(oldService.Id, oldService);
            var resource = _mapper.Map<Resource>(resourceModel);
            resource.IsDeleted = false;
            var newResource = await _resourceRepository.AddResource(resource);
            return newResource;
        }

        public async Task<ResourceModel> GetResourceById(int id)
        {
            _logger.LogInformation($"Request for getting a resource by id {id}");
            var resource = await _resourceRepository.GetResourceById(id);
            CheckErrorHelper.CheckIfEntityIsNull(id, resource);
            return _mapper.Map<ResourceModel>(resource);
        }


        public async Task SoftDelete(int id, ResourceModel resourceModel)
        {
            _logger.LogInformation($"Request for soft deletion of resource by id {id}");
            var resource = await _resourceRepository.GetResourceById(id);
            CheckErrorHelper.CheckIfEntityIsNull(id, resource);
            resourceModel.Id = id;
            resourceModel.Name = resource.Name;
            var newResource =  _mapper.Map<Resource>(resourceModel);
            await _resourceRepository.SoftDelete(newResource);
        }

        public async Task UpdateResource(int id, ResourceModel resourceModel)
        {
            _logger.LogInformation($"Request for updating a status of Resource by id {id}");
            var oldResource = await _resourceRepository.GetResourceById(id);
            CheckErrorHelper.CheckIfEntityIsNull(id, oldResource);
            resourceModel.Id = id;
            var resource = _mapper.Map<Resource>(resourceModel);
            await _resourceRepository.UpdateResource(resource);
        }

        public async Task<List<ResourceModel>> GetAllResources()
        {
            _logger.LogInformation("Request for getting all resources");
            var resources = await _resourceRepository.GetAllResources();
            return _mapper.Map<List<ResourceModel>>(resources);
        }

        public async Task<List<ResourceModel>> GetActiveResourceService()
        {
            _logger.LogInformation("Request for getting all active resources");
            var resourse = await _resourceRepository.GetAllResources();
            var tmp = _mapper.Map<List<ResourceModel>>(resourse);
            tmp.RemoveAll(t => t.IsDeleted == true);
            CheckErrorHelper.CheckIfResourceModelCountIsZero(tmp);

            return _mapper.Map<List<ResourceModel>>(tmp);
        }
    }
}
