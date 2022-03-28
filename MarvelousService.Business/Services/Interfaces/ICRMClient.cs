using MarvelousService.BusinessLayer.Models;

namespace MarvelousService.BusinessLayer.Services.Interfaces
{
    public interface ICRMClient
    {
        Task<string> GetToken(AuthModel authModel);
        Task<int> RegistrateLead(LeadModel leadModel);
    }
}
