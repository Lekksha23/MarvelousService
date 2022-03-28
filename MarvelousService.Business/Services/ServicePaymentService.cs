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

        public async Task<int> AddServicePayment(ServicePaymentModel servicePaymentModel)
        {
            _logger.LogInformation("Query for adding service payment");
            var servicePayment = _mapper.Map<ServicePayment>(servicePaymentModel);
            var id = await _servicePaymentRepository.AddServicePayment(servicePayment);
            _logger.LogInformation($"Service payment was added. ServiceToLeadId = {servicePayment.ServiceToLeadId}");
            return id;
        }

        public async Task<List<ServicePaymentModel>> GetServicePaymentsById(int serviceToLeadId)
        {
            _logger.LogInformation("Query for receiving service payments by id");
            var servicePayments = await _servicePaymentRepository.GetServicePaymentsByServiceToLeadId(serviceToLeadId);
            CheckServicePayments(servicePayments);
            return _mapper.Map<List<ServicePaymentModel>>(servicePayments);
        }

        private void CheckServicePayments(List<ServicePayment> servicePayments)
        {
            if (servicePayments is null)
            {
                _logger.LogError("Error in receiving information about service payment");
                throw new NotFoundServiceException("Service payment not found");
            }
        }
    }
}
