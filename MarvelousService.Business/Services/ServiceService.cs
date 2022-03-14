using AutoMapper;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Services.Interfaces;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Interfaces;
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

        public List<ServiceModel> GetByLeadId(int id)
        {
            
            _logger.Debug("запрос на получение лида по id");

            var lead = _serviceRepository.GetByLeadId(id);

            return _mapper.Map<List<ServiceModel>>(lead);

        }

        public ServiceModel GetServiceById(int id)
        {
            _logger.Debug("запрос на получение услуги по id");

            var service = _serviceRepository.GetServiceById(id);

            return  _mapper.Map<ServiceModel>(service);
        }
    }
}
