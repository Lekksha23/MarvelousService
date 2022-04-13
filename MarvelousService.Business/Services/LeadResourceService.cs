using AutoMapper;
using Marvelous.Contracts.Enums;
using MarvelousService.BusinessLayer.Helpers;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Clients.Interfaces;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Enums;
using MarvelousService.DataLayer.Repositories;

namespace MarvelousService.BusinessLayer.Clients
{
    public class LeadResourceService : ILeadResourceService
    {
        private readonly ILeadResourceRepository _leadResourceRepository;
        private readonly IResourcePaymentRepository _resourcePaymentRepository;
        private readonly ICRMService _crmService;
        private readonly ITransactionService _transactionService;
        private IRoleStrategy _roleStrategy;
        private readonly IRoleStrategyProvider _roleStrategyProvider;
        private readonly IMapper _mapper;
        private readonly ICheckErrorHelper _helper;

        public LeadResourceService(
            ILeadResourceRepository LeadResourceRepository,
            IResourcePaymentRepository resourcePaymentRepository,
            ITransactionService transactionService,
            ICRMService crmService,
            IRoleStrategy roleStrategy,
            IRoleStrategyProvider roleStrategyProvider,
            IMapper mapper,
            ICheckErrorHelper helper)
        {
            _leadResourceRepository = LeadResourceRepository;
            _resourcePaymentRepository = resourcePaymentRepository;
            _transactionService = transactionService;
            _crmService = crmService;
            _roleStrategy = roleStrategy;
            _roleStrategyProvider = roleStrategyProvider;
            _mapper = mapper;
            _helper = helper;
        }

        public async Task<int> AddLeadResource(LeadResourceModel leadResourceModel, Role role, string jwtToken)
        {
            leadResourceModel.Price = leadResourceModel.CountPrice();
            GiveDiscountIfLeadIsVip(leadResourceModel, role);  
            var leadResource = _mapper.Map<LeadResource>(leadResourceModel);
            var accountId = await _crmService.GetIdOfRubLeadAccount(jwtToken);
            var transactionId = await _transactionService.AddResourceTransaction(accountId, leadResourceModel.Price);
            leadResource.Status = Status.Active;
            var leadResourceId = await _leadResourceRepository.AddLeadResource(leadResource);
            await _resourcePaymentRepository.AddResourcePayment(leadResourceId, transactionId);
            return leadResourceId;
        }

        public async Task<List<LeadResourceModel>> GetByLeadId(int id)
        {
            var leadResources = await _leadResourceRepository.GetByLeadId(id);
            _helper.CheckIfEntityIsNull(id, leadResources);
            return _mapper.Map<List<LeadResourceModel>>(leadResources);
        }

        public async Task<List<LeadResourceModel>> GetByPayDate(DateTime payDate)
        {
            var leadResources = await _leadResourceRepository.GetByPayDate(payDate);
            return _mapper.Map<List<LeadResourceModel>>(leadResources);
        }

        public async Task<List<LeadResourceModel>> GetById(int id)
        {
            var leadResource = await _leadResourceRepository.GetLeadResourceById(id);
            _helper.CheckIfEntityIsNull(id, leadResource);
            return _mapper.Map<List<LeadResourceModel>>(leadResource);
        }

        private void GiveDiscountIfLeadIsVip(LeadResourceModel leadResourceModel, Role role)
        {
            _roleStrategy = _roleStrategyProvider.GetStrategy((int)role);
            _roleStrategy.GiveLeadDiscount(leadResourceModel, role);
        }
    }
}
