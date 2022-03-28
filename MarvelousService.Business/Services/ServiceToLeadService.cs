using AutoMapper;
using Marvelous.Contracts;
using MarvelousService.BusinessLayer.Exceptions;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Services.Interfaces;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Enums;
using MarvelousService.DataLayer.Repositories;
using MarvelousService.DataLayer.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace MarvelousService.BusinessLayer.Services
{
    public class ServiceToLeadService : IServiceToLeadService
    {
        private readonly IServiceToLeadRepository _serviceToLeadRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IServicePaymentRepository _servicePaymentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceToService> _logger;
        private readonly ITransactionStoreClient _transactionStoreClient;

        private const double _discountVIP = 0.9;

        public ServiceToLeadService(IServiceToLeadRepository serviceToLeadRepository,
            IMapper mapper,
            ILogger<ServiceToService> logger,
            IServiceRepository serviceRepository,
            ITransactionStoreClient transactionService)
        {
            _serviceToLeadRepository = serviceToLeadRepository;
            _serviceRepository = serviceRepository;
            _transactionStoreClient = transactionService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> AddServiceToLead(ServiceToLeadModel serviceToLeadModel, int role)
        {
            var service = await _serviceRepository.GetServiceById(serviceToLeadModel.ServiceId);
            //var totalPrice = serviceToLeadModel.GetPrice(service.Price);

            //if (role == (int)Role.Vip)
            //{
            //    serviceToLeadModel.Price = totalPrice * (decimal)_discountVIP;
            //}
            //else
            //{
            //    serviceToLeadModel.Price = totalPrice;
            //}
            var serviceToLead = _mapper.Map<ServiceToLead>(serviceToLeadModel);
            //get account by leadid
            var serviceTransactionModel = new TransactionRequestModel {
                Amount = serviceToLeadModel.Price,
                Currency = Currency.RUB,
                AccountId = 23
            };
            var transactionId = _transactionStoreClient.AddTransaction(serviceTransactionModel);
            var servicePayment = new ServicePayment {
                ServiceToLeadId = serviceToLead,
                TransactionId = transactionId.Id
            };
            await _servicePaymentRepository.AddServicePayment(servicePayment);
            serviceToLeadModel.Status = Status.Active;

            _logger.LogInformation($"Query for adding service with id = {service.Id} to Lead with id = {serviceToLead.LeadId}");
            return await _serviceToLeadRepository.AddServiceToLead(serviceToLead);
        }

        public async Task<List<ServiceToLeadModel>> GetLeadById(int id)
        {
            _logger.LogInformation("Запрос на получение лида по id");
            var lead = await _serviceToLeadRepository.GetByLeadId(id);

            if (lead == null)
            {
                _logger.LogError("Ошибка в получении лида по Id ");
                throw new NotFoundServiceException("Такого лида не существует.");
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
            return _mapper.Map<List<ServiceToLeadModel>>(service);
        }
    }
}
