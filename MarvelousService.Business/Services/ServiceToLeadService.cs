using AutoMapper;
using MarvelousService.BusinessLayer.Exceptions;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Services.Interfaces;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Interfaces;
using NLog;

namespace MarvelousService.BusinessLayer.Services
{
    public class ServiceToLeadService : IServiceToLeadService
    {
        private readonly IServiceToLeadRepository _serviceToLeadRepository;
        private readonly IMapper _mapper;
        private readonly Logger _logger;

        public ServiceToLeadService(IServiceToLeadRepository serviceToLeadRepository, IMapper mapper)
        {
            _serviceToLeadRepository = serviceToLeadRepository;
            _mapper = mapper;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public int AddServiceToLead(ServiceToLeadModel serviceToLeadModel)
        {

            var service = _mapper.Map<ServiceToLead>(serviceToLeadModel);

            _logger.Info("запрос на добавление услуги");

            return _serviceToLeadRepository.AddServiceToLead(service);
        }

        public List<ServiceToLeadModel> GetLeadById(int id)
        {
                
            _logger.Info("запрос на получение лида по id");

            var lead = _serviceToLeadRepository.GetByLeadId(id);

            if (lead == null)
            {
                _logger.Error("Ошибка в получении лида по Id ");

                throw new NotFoundServiceException("Такого  лида не существует.");
            }
                

            return _mapper.Map<List<ServiceToLeadModel>>(lead);

        }

        public ServiceToLeadModel GetServiceToLeadById(int id)
        {
            _logger.Info("запрос на получение услуги по id");

            var service = _serviceToLeadRepository.GetServiceToLeadById(id);

            if (service == null)
            {
                _logger.Error("Ошибка в получении услуги по Id ");

                throw new NotFoundServiceException("Такой услуги не существует.");
            }    
                

            return  _mapper.Map<ServiceToLeadModel>(service);
        }
    }
}
