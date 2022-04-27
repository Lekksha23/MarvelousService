﻿using Marvelous.Contracts.Enums;
using MarvelousService.BusinessLayer.Models;


namespace MarvelousService.BusinessLayer.Clients.Interfaces
{
    public interface ILeadResourceService
    {
        Task<int> AddLeadResource(LeadResourceModel serviceModel, Role role, string jwtToken);
        Task<LeadResourceModel>GetById(int id);
        Task<List<LeadResourceModel>> GetByLeadId(int id);
        Task<List<LeadResourceModel>> GetByPayDate(DateTime payDate);
    }
}
