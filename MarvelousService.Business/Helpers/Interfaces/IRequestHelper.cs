using Marvelous.Contracts.Enums;
using Marvelous.Contracts.RequestModels;
using Marvelous.Contracts.ResponseModels;
using RestSharp;

namespace MarvelousService.BusinessLayer.Helpers
{
    public interface IRequestHelper
    {
        Task<RestResponse<T>> GetTokenForFront<T>(Microservice service, AuthRequestModel authReguest);
        Task<RestResponse<T>> SendRequest<T>(string path, Microservice service, string jwtToken = "null");
        void CheckMicroserviceResponse(RestResponse response);
        Task<RestResponse<IdentityResponseModel>> SendRequestToValidateToken(string jwtToken);
        Task<IdentityResponseModel> GetLeadIdentityByToken(string jwtToken);
    }
}