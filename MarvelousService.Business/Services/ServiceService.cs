using AutoMapper;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Services.Interfaces;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Repositories.Interfaces;
using NLog;

namespace MarvelousService.BusinessLayer.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        private readonly Logger _logger;

        public ServiceService(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
            _logger = LogManager.GetCurrentClassLogger();
        }
        public int AddService(ServiceModel serviceModel)
        {
            var service = _mapper.Map<Service>(serviceModel);

            _logger.Debug("запрос на добавление услуги");

            return _serviceRepository.AddService(service);
        }

        public ServiceModel GetServiceById(int id)
        {
            _logger.Debug("запрос на получение услуги по id");

            var lead = _serviceRepository.GetServiceById(id);

            return _mapper.Map<ServiceModel>(lead);
        }

        public ServiceModel SoftDeleted(ServiceModel serviceModel)
        {
            var service = _mapper.Map<Service>(serviceModel);

            _logger.Debug("запрос на удаление услуги");

            return (ServiceModel)_serviceRepository.SoftDeleted(service);
        }

        public ServiceModel UpdateService(ServiceModel serviceModel)
        {
            var service = _mapper.Map<Service>(serviceModel);

            _logger.Debug("запрос на изменение услуги");

            return (ServiceModel)_serviceRepository.UpdateService(service);
        }
    }
}
