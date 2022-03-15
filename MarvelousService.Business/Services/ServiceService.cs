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
            _logger.Debug("запрос на добавление услуги");

            var service = _mapper.Map<Service>(serviceModel);

            var newService =  _serviceRepository.AddService(service);

            return newService;
        }

        public ServiceModel GetServiceById(int id)
        {
            _logger.Debug("запрос на получение услуги по id");

            var lead = _serviceRepository.GetServiceById(id);

            return _mapper.Map<ServiceModel>(lead);
        }

        public Service SoftDeleted(ServiceModel serviceModel)
        {
            _logger.Debug("запрос на удаление услуги");

            var service = _mapper.Map<Service>(serviceModel);

            var newService = _serviceRepository.SoftDeleted(service);

            return newService;
        }

        public int UpdateService(ServiceModel serviceModel)
        {
            _logger.Debug("запрос на изменение услуги");

            var service = _mapper.Map<Service>(serviceModel);

            var newService = _serviceRepository.UpdateService(service);

            return newService;
        }
    }
}
