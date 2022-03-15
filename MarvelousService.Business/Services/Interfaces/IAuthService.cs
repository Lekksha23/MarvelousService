using MarvelousService.BusinessLayer.Models;
using RestSharp;

namespace MarvelousService.BusinessLayer.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> GetToken(AuthModel authModel);
        Task<RestResponse> RegistrateLead(LeadModel leadModel);
    }
}
