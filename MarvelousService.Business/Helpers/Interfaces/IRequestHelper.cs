using Marvelous.Contracts.Enums;
using Marvelous.Contracts.RequestModels;
using Marvelous.Contracts.ResponseModels;
using RestSharp;

namespace MarvelousService.BusinessLayer.Helpers
{
    public interface IRequestHelper
    {
        Task<string> GetTokenForFront(Microservice service, AuthRequestModel authReguest);
        Task<RestResponse<T>> SendRequest<T>(string path, Microservice service, string jwtToken = "null");
        void CheckMicroserviceResponse(RestResponse response);
        Task<IdentityResponseModel>SendRequestToValidateToken(string jwtToken);
    }
}