using AutoMapper;
using Marvelous.Contracts.Enums;
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
        private readonly ILogger<LeadResourceService> _logger;
        private readonly IMapper _mapper;
        private readonly IHelper _helper;

        private const double _discountVIP = 0.9;

        public LeadResourceService(
            ILeadResourceRepository LeadResourceRepository,
            IResourceRepository resourceRepository,
            IResourcePaymentRepository resourcePaymentRepository,
            ITransactionService transactionService,
            ICRMService crmService,
            ILogger<LeadResourceService> logger,
            IMapper mapper,
            IHelper helper)
        {
            _leadResourceRepository = LeadResourceRepository;
            _resourceRepository = resourceRepository;
            _resourcePaymentRepository = resourcePaymentRepository;
            _transactionService = transactionService;
            _crmService = crmService;
            _mapper = mapper;
            _logger = logger;
            _helper = helper;
        }

        public async Task<int> AddLeadResource(LeadResourceModel leadResourceModel, int role)
        {
            _logger.LogInformation($"Query for adding a resource with id {leadResourceModel.Resource.Id} to Lead with id {leadResourceModel.LeadId}.");
            var resource = await _resourceRepository.GetResourceById(leadResourceModel.Resource.Id);
            _helper.CheckResource(resource);
            var totalPrice = leadResourceModel.GetPrice(resource.Price, leadResourceModel.Period);
            CheckRole(leadResourceModel, totalPrice, role);
            var leadResource = _mapper.Map<LeadResource>(leadResourceModel);
            var accountId = await _crmService.GetIdOfRubLeadAccount();
            var transactionId = await _transactionService.AddResourceTransaction(accountId, leadResourceModel.Price);
            await _resourcePaymentRepository.AddResourcePayment(leadResource, transactionId);
            leadResource.Status = Status.Active;
            return await _leadResourceRepository.AddLeadResource(leadResource);
        }

        public async Task<List<LeadResourceModel>> GetByLeadId(int id)
        {
            _logger.LogInformation($"Request for getting a LeadResource by LeadId {id}.");
            var lead = await _leadResourceRepository.GetByLeadId(id);

            if (lead == null)
            {
                _logger.LogError("Error in getting lead by Id.");
                throw new NotFoundServiceException("This lead does not exist.");
            }
            return _mapper.Map<List<LeadResourceModel>>(lead);
        }

        public async Task<List<LeadResourceModel>> GetLeadResourceById(int id)
        {
            _logger.LogInformation("Resource request by id.");
            var leadResource = await _leadResourceRepository.GetLeadResourceById(id);

            if (leadResource == null)
            {
                _logger.LogError("Error in receiving LeadResource by Id.");
                throw new NotFoundServiceException("This LeadResource does not exist.");
            }
            return _mapper.Map<List<LeadResourceModel>>(leadResource);
        }

        private void CheckRole(LeadResourceModel leadResourceModel, decimal price, int role)
        {
            switch (role)
            {
                case (int)Role.Vip:
                    leadResourceModel.Price = price * (decimal)_discountVIP;
                    break;
                case (int)Role.Regular:
                    leadResourceModel.Price = price;
                    break;
                case (int)Role.Admin:
                    throw new RoleException("User with role Admin can't buy any resources.");
                    _logger.LogError("User with role Admin was trying to buy a resource.");
                    break;
                default:
                    throw new RoleException("Unknown role.");
                    _logger.LogError("User with unknown role was trying to buy a resource.");
            }
        }
    }
}
