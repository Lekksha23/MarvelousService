using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Models.CRMModels;

namespace MarvelousService.BusinessLayer.Services
{
    public interface ICRMClient
    {
        Task<int> AddLead(LeadModel lead);
        Task Authorize(AuthModel authModel);
        Task<List<AccountModel>> GetLeadAccounts();
    }
}