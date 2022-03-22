using AutoMapper;
using MarvelousService.BusinessLayer.Exceptions;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Services.Interfaces;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Interfaces;
using Microsoft.Extensions.Logging;

namespace MarvelousService.BusinessLayer.Services
{
    public class ServiceToLeadService : IServiceToLeadService
    {
        private readonly IServiceToLeadRepository _serviceToLeadRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceToService> _logger;

        public ServiceToLeadService(IServiceToLeadRepository serviceToLeadRepository, IMapper mapper, ILogger<ServiceToService> logger)
        {
            _serviceToLeadRepository = serviceToLeadRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> AddServiceToLead(ServiceToLeadModel serviceToLeadModel)
        {
            var service = _mapper.Map<ServiceToLead>(serviceToLeadModel);
            _logger.LogInformation("запрос на добавление услуги");
            return await _serviceToLeadRepository.AddServiceToLead(service);
        }

        public async Task<List<ServiceToLeadModel>> GetLeadById(int id)
        {
            _logger.LogInformation("запрос на получение лида по id");
            var lead = await _serviceToLeadRepository.GetByLeadId(id);

            if (lead == null)
            {
                _logger.LogError("Ошибка в получении лида по Id ");
                throw new NotFoundServiceException("Такого  лида не существует.");
            }
            return _mapper.Map<List<ServiceToLeadModel>>(lead);
        }

        public async Task<List<ServiceToLeadModel>> GetServiceToLeadById(int id)
        {
            _logger.LogInformation("запрос на получение услуги по id");
            var service = await _serviceToLeadRepository.GetServiceToLeadById(id);

            if (service == null)
            {
                _logger.LogError("Ошибка в получении услуги по Id ");
                throw new NotFoundServiceException("Такой услуги не существует.");
            }    
            return  _mapper.Map<List<ServiceToLeadModel>>(service);
        }
    }
}
