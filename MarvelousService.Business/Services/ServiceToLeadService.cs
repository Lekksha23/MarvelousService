using AutoMapper;
using Marvelous.Contracts.Enums;
using Marvelous.Contracts.RequestModels;
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
        private readonly ILogger<ServiceToLeadService> _logger;
        private readonly ITransactionStoreClient _transactionStoreClient;
        private readonly ICRMClient _crmClient;

        private const double _discountVIP = 0.9;

        public ServiceToLeadService(IServiceToLeadRepository serviceToLeadRepository,
            IMapper mapper,
            ILogger<ServiceToLeadService> logger,
            IServiceRepository serviceRepository,
            ITransactionStoreClient transactionService,
            ICRMClient crmClient)
        {
            _serviceToLeadRepository = serviceToLeadRepository;
            _serviceRepository = serviceRepository;
            _transactionStoreClient = transactionService;
            _crmClient = crmClient;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> AddServiceToLead(ServiceToLeadModel serviceToLeadModel, int role)
        {
            var service = await _serviceRepository.GetServiceById(serviceToLeadModel.ServiceId.Id);
            var totalPrice = serviceToLeadModel.GetTotalPrice(service.Price, serviceToLeadModel.Period);
            CheckRole(serviceToLeadModel, totalPrice, role);
            var serviceToLead = _mapper.Map<ServiceToLead>(serviceToLeadModel);
            var leadAccounts = await _crmClient.GetAccountsByLeadId();
            var accountId = 0;
            var count = 0;

            for (int i = 0; i < leadAccounts.Count; i++)
            {
                if (leadAccounts[i].CurrencyType == Currency.RUB)
                {
                    accountId = leadAccounts[i].Id;
                    count++;
                    break;
                }
            }
            if (count == 0)
            {
                throw new AccountException(
                    $"There's no accounts with RUB CurrencyType was found in CRM for Lead with id {serviceToLeadModel.LeadId}");
            }
            var serviceTransactionModel = new TransactionRequestModel {
                Amount = serviceToLeadModel.Price,
                Currency = Currency.RUB,
                AccountId = accountId
            };
            var transactionId = await _transactionStoreClient.AddTransaction(serviceTransactionModel);
            var servicePayment = new ServicePayment {
                ServiceToLeadId = serviceToLead,
                TransactionId = transactionId
            };
            await _servicePaymentRepository.AddServicePayment(servicePayment);
            serviceToLead.Status = Status.Active;

            _logger.LogInformation($"Query for adding service with id {service.Id} to Lead with id {serviceToLead.LeadId}");
            return await _serviceToLeadRepository.AddServiceToLead(serviceToLead);
        }

        public async Task<List<ServiceToLeadModel>> GetByLeadId(int id)
        {
            _logger.LogInformation("Lead request by id");
            var lead = await _serviceToLeadRepository.GetByLeadId(id);

            if (lead == null)
            {
                _logger.LogError("Error in getting lead by Id ");
                throw new NotFoundServiceException("Such a lead does not exist.");
            }
            return _mapper.Map<List<ServiceToLeadModel>>(lead);
        }

        public async Task<List<ServiceToLeadModel>> GetServiceToLeadById(int id)
        {
            _logger.LogInformation("Service request by id");
            var service = await _serviceToLeadRepository.GetServiceToLeadById(id);

            if (service == null)
            {
                _logger.LogError("Error in receiving service by Id ");
                throw new NotFoundServiceException("This service does not exist.");
            }
            return _mapper.Map<List<ServiceToLeadModel>>(service);
        }

        private void CheckRole(ServiceToLeadModel serviceToLeadModel, decimal totalPrice, int role)
        {
            switch (role)
            {
                case (int)Role.Vip:
                    serviceToLeadModel.Price = totalPrice * (decimal)_discountVIP;
                    break;
                case (int)Role.Regular:
                    serviceToLeadModel.Price = totalPrice;
                    break;
                case (int)Role.Admin:
                    throw new RoleException("User with role admin can't book any services");
                    _logger.LogError("User with role admin was trying to book a service");
                    break;
                default:
                    throw new RoleException("Unknown role");
                    _logger.LogError("User with unknown role was trying to book a service");
            }
        }
    }
}
