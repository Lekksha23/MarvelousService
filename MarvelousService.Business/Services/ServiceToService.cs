using AutoMapper;
using MarvelousService.BusinessLayer.Exceptions;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Services.Interfaces;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Repositories.Interfaces;
using NLog;

namespace MarvelousService.BusinessLayer.Services
{
    public class ServiceToService : IServiceToService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        private readonly Logger _logger;

        public ServiceToService(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public int AddService(ServiceModel serviceModel)
        {
            _logger.Info("запрос на добавление услуги");
            var service = _mapper.Map<Service>(serviceModel);
            var newService =  _serviceRepository.AddService(service);
            return newService;
        }

        public ServiceModel GetServiceById(int id)
        {
            _logger.Info("запрос на получение услуги по id");
           
            var service = _serviceRepository.GetServiceById(id);

            if (service == null)
            {
                _logger.Error("Ошибка в получении услуги по Id ");

                throw new NotFoundServiceException("Такой услуги не существует.");
            }

            return _mapper.Map<ServiceModel>(service);
        }

        public void SoftDeleted(int id, ServiceModel serviceModel)
        {
            _logger.Info("запрос на удаление услуги");

            var oldService = _serviceRepository.GetServiceById(id);

            if (oldService == null)
            {
                _logger.Error("Ошибка в получении услуги по Id ");

                throw new NotFoundServiceException("Такой услуги не существует.");
            }

            var service = _mapper.Map<Service>(serviceModel);
            _serviceRepository.SoftDeleted(id, service);
        }

        public void UpdateService(int id, ServiceModel serviceModel)
        {
            _logger.Info("запрос на изменение услуги");

            var oldService = _serviceRepository.GetServiceById(id);

            if (oldService == null)
            {
                _logger.Error("Ошибка в получении услуги по Id ");

                throw new NotFoundServiceException("Такой услуги не существует.");
            }

            var service = _mapper.Map<Service>(serviceModel);
            _serviceRepository.UpdateService(id, service);
        }
    }
}
