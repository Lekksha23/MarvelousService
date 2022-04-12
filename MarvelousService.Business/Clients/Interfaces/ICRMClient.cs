using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Models.CRMModels;

namespace MarvelousService.BusinessLayer.Clients
{
    public interface ICRMClient
    {
        Task<int> AddLead(LeadModel lead);
        Task<List<AccountModel>> GetLeadAccounts(string jwtToken);
    }
}