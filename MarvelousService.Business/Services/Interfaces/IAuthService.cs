using MarvelousService.BusinessLayer.Models;
using RestSharp;

namespace MarvelousService.BusinessLayer.Services.Interfaces
{
    public interface IAuthService
    {
        Task<RestResponse> GetToken(AuthModel authModel);
    }
}
