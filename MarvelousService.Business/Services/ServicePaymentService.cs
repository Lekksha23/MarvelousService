using AutoMapper;
using MarvelousService.BusinessLayer.Exceptions;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Repositories;
using Microsoft.Extensions.Logging;

namespace MarvelousService.BusinessLayer.Services
{
    public class ServicePaymentService : IServicePaymentService
    {
        private readonly IServicePaymentRepository _servicePaymentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ServicePaymentService> _logger;

        public ServicePaymentService(IServicePaymentRepository servicePaymentRepository, IMapper mapper, ILogger<ServicePaymentService> logger)
        {
            _servicePaymentRepository = servicePaymentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> AddServicePayment(ServicePayment servicePaymentModel)
        {
            _logger.LogInformation("Запрос на добавление оплаты услуги");
            var servicePayment = _mapper.Map<ServicePayment>(servicePaymentModel);
            var id = await _servicePaymentRepository.AddServicePayment(servicePayment);
            _logger.LogInformation($"Оплата услуги добавлена. ServiceToLeadId = {servicePayment.ServiceToLeadId}");
            return id;
        }

        public async Task<List<ServicePaymentModel>> GetServiceById(int serviceToLeadId)
        {
            _logger.LogInformation("запрос на получение услуги по id");
            var servicePayments = await _servicePaymentRepository.GetServicePaymentsByServiceToLeadId(serviceToLeadId);
            CheckServicePayments(servicePayments);
            return _mapper.Map<List<ServicePaymentModel>>(servicePayments);
        }

        private void CheckServicePayments(List<ServicePayment> servicePayments)
        {
            if (servicePayments.Count == 0)
            {
                _logger.LogError("Ошибка в получении информации об оплате услуги");
                throw new NotFoundServiceException("Оплата для услуги не найдена");
            }
        }
    }
}
