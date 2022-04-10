﻿using AutoMapper;
using Marvelous.Contracts.Enums;
using MarvelousService.BusinessLayer.Helpers;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Services.Interfaces;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Enums;
using MarvelousService.DataLayer.Repositories;

namespace MarvelousService.BusinessLayer.Services
{
    public class LeadResourceService : ILeadResourceService
    {
        private readonly ILeadResourceRepository _leadResourceRepository;
        private readonly IResourcePaymentRepository _resourcePaymentRepository;
        private readonly ICRMService _crmService;
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;
        private readonly IHelper _helper;
        private IRoleStrategy _roleStrategy;
        private readonly IRoleStrategyProvider _roleStrategyProvider;

        public LeadResourceService(
            ILeadResourceRepository LeadResourceRepository,
            IResourcePaymentRepository resourcePaymentRepository,
            ITransactionService transactionService,
            ICRMService crmService,
            IRoleStrategy roleStrategy,
            IRoleStrategyProvider roleStrategyProvider,
            IMapper mapper,
            IHelper helper)
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

        public async Task<int> AddLeadResource(LeadResourceModel leadResourceModel, Role role)
        {
            leadResourceModel.Price = leadResourceModel.CountPrice();
            GiveDiscountIfLeadIsVIP(leadResourceModel, role);  
            var leadResource = _mapper.Map<LeadResource>(leadResourceModel);
            var accountId = await _crmService.GetIdOfRubLeadAccount();
            var transactionId = await _transactionService.AddResourceTransaction(accountId, leadResourceModel.Price);
            await _resourcePaymentRepository.AddResourcePayment(leadResource, transactionId);
            leadResource.Status = Status.Active;
            return await _leadResourceRepository.AddLeadResource(leadResource);
        }

        public async Task<List<LeadResourceModel>> GetByLeadId(int id)
        {
            var leadResource = await _leadResourceRepository.GetByLeadId(id);
            _helper.CheckIfEntityIsNull(id, leadResource);
            return _mapper.Map<List<LeadResourceModel>>(leadResource);
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

        private void GiveDiscountIfLeadIsVIP(LeadResourceModel leadResourceModel, Role role)
        {
            _roleStrategy = _roleStrategyProvider.GetStrategy((int)role);
            _roleStrategy.GiveLeadDiscount(leadResourceModel, role);
        }
    }
}
