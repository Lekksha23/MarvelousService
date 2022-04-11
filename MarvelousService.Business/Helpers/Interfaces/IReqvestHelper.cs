using Marvelous.Contracts.Enums;
using Marvelous.Contracts.RequestModels;
using Marvelous.Contracts.ResponseModels;
using RestSharp;

namespace MarvelousService.BusinessLayer.Helpers
{
    public interface IReqvestHelper
    {
        Task<RestResponse<T>> GetTokenFromFront<T>(Microservice service, AuthRequestModel authReguest);
        Task<RestResponse<T>> SendRequest<T>(string path, Microservice service, string jwtToken = "null");
        Task<RestResponse<IdentityResponseModel>> SendRequestCheckValidateToken(string jwtToken);
    }
}