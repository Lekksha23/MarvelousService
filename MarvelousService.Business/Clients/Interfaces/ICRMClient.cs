using MarvelousService.BusinessLayer.Models;

namespace MarvelousService.BusinessLayer.Clients
{
    public interface ICRMClient
    {
        Task<int> AddLead(LeadModel lead);
        Task<List<AccountModel>> GetLeadAccounts(string jwtToken);
    }
}