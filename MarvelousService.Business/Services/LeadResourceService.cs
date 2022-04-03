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
    public class LeadResourceService : ILeadResourceService
    {
        private readonly ILeadResourceRepository _leadResourceRepository;
        private readonly IResourceRepository _resourceRepository;
        private readonly IResourcePaymentRepository _resourcePaymentRepository;
        private readonly ICRMService _crmService;
        private readonly ITransactionService _transactionService;
        private readonly ITransactionStoreClient _transactionStoreClient;
        private readonly ICRMClient _crmClient;
        private readonly IMapper _mapper;
        private readonly ILogger<LeadResourceService> _logger;

        private const double _discountVIP = 0.9;

        public LeadResourceService(
            ILeadResourceRepository LeadResourceRepository,
            IResourceRepository resourceRepository,
            IResourcePaymentRepository resourcePaymentRepository,
            ITransactionService transactionService,
            ITransactionStoreClient transactionClient,
            ICRMService crmService,
            ICRMClient crmClient,
            IMapper mapper,
            ILogger<LeadResourceService> logger)
        {
            _leadResourceRepository = LeadResourceRepository;
            _resourceRepository = resourceRepository;
            _resourcePaymentRepository = resourcePaymentRepository;
            _transactionService = transactionService;
            _transactionStoreClient = transactionClient;
            _crmService = crmService;
            _crmClient = crmClient;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> AddLeadResource(LeadResourceModel leadResourceModel, int role)
        {
            _logger.LogInformation($"Query for adding a resource with id {leadResourceModel.Resource.Id} to Lead with id {leadResourceModel.LeadId}");
            var resource = await _resourceRepository.GetResourceById(leadResourceModel.Resource.Id);
            var totalPrice = leadResourceModel.GetTotalPrice(resource.Price, leadResourceModel.Period);
            CheckRole(leadResourceModel, totalPrice, role);
            var leadResource = _mapper.Map<LeadResource>(leadResourceModel);
            _logger.LogInformation("Query for getting a RUB LeadAccount");
            var accountId = await _crmService.GetIdOfRubLeadAccount();
            _logger.LogInformation("Query for adding a transaction for ResourcePayment");
            var transactionId = await _transactionService.AddResourceTransaction(accountId, leadResourceModel.Price);
            var resourcePayment = new ResourcePayment 
            {
                LeadResource = leadResource,
                TransactionId = transactionId
            };
            _logger.LogInformation("Query for adding a ResourcePayment");
            await _resourcePaymentRepository.AddResourcePayment(resourcePayment);
            leadResource.Status = Status.Active;
            return await _leadResourceRepository.AddLeadResource(leadResource);
        }

        public async Task<List<LeadResourceModel>> GetByLeadId(int id)
        {
            _logger.LogInformation($"Request for getting a LeadResource by LeadId {id}");
            var lead = await _leadResourceRepository.GetByLeadId(id);

            if (lead == null)
            {
                _logger.LogError("Error in getting lead by Id ");
                throw new NotFoundServiceException("This lead does not exist.");
            }
            return _mapper.Map<List<LeadResourceModel>>(lead);
        }

        public async Task<List<LeadResourceModel>> GetLeadResourceById(int id)
        {
            _logger.LogInformation("Resource request by id");
            var resource = await _leadResourceRepository.GetLeadResourceById(id);

            if (resource == null)
            {
                _logger.LogError("Error in receiving resource by Id ");
                throw new NotFoundServiceException("This resource does not exist.");
            }
            return _mapper.Map<List<LeadResourceModel>>(resource);
        }

        private void CheckRole(LeadResourceModel leadResourceModel, decimal totalPrice, int role)
        {
            switch (role)
            {
                case (int)Role.Vip:
                    leadResourceModel.Price = totalPrice * (decimal)_discountVIP;
                    break;
                case (int)Role.Regular:
                    leadResourceModel.Price = totalPrice;
                    break;
                case (int)Role.Admin:
                    throw new RoleException("User with role admin can't buy any resources");
                    _logger.LogError("User with role admin was trying to buy a resource");
                    break;
                default:
                    throw new RoleException("Unknown role");
                    _logger.LogError("User with unknown role was trying to buy a resource");
            }
        }
    }
}
