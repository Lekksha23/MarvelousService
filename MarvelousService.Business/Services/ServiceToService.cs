using AutoMapper;
using Marvelous.Contracts;
using MarvelousService.BusinessLayer.Exceptions;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Services.Interfaces;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace MarvelousService.BusinessLayer.Services
{
    public class ServiceToService : IServiceToService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceToService> _logger;
        private readonly Helper _helper;

        public ServiceToService(IServiceRepository serviceRepository, IMapper mapper, ILogger<ServiceToService> logger)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> AddService(ServiceModel serviceModel)
        {
            _logger.LogInformation("Request for adding service");
            var service = _mapper.Map<Service>(serviceModel);
            service.IsDeleted = false;
            var newService = await _serviceRepository.AddService(service);
            return newService;
        }

        public async Task<ServiceModel> GetServiceById(int id)
        {
            _logger.LogInformation("Request for getting service by id");
            var service = await _serviceRepository.GetServiceById(id);
            _helper.CheckService(service);
            return _mapper.Map<ServiceModel>(service);
        }

        public async Task<List<ServiceModel>> GetAllServiceModels()
        {
            _logger.LogInformation("Request for getting services");
            var services = await _serviceRepository.GetAllService();
            return _mapper.Map<List<ServiceModel>>(services);

        }

        public async Task SoftDelete(int id, ServiceModel serviceModel)
        {
            _logger.LogInformation("Request for soft deletion service by id");
            _helper.CheckService(oldService);
            var service = await _serviceRepository.GetServiceById(id);
            CheckService(service);
            serviceModel.Id = id;
            serviceModel.Name = service.Name;
            var newService =  _mapper.Map<Service>(serviceModel);
            await _serviceRepository.SoftDelete(newService);
        }

        public async Task UpdateService(int id, ServiceModel serviceModel)
        {
            _logger.LogInformation("Service update request");
            var oldService = await _serviceRepository.GetServiceById(id);
            _helper.CheckService(oldService);
            serviceModel.Id = id;
            _logger.LogInformation("Service update request was successful");
            var service = _mapper.Map<Service>(serviceModel);
            await _serviceRepository.UpdateService(service);
        }



    }

}
