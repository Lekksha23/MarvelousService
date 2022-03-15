using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Services.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace MarvelousService.BusinessLayer.Services
{
    public class AuthService : IAuthService
    {
        private const string _url = "https://piter-education.ru:5050";
        private const string _loginPath = "/login/";

        public Task<RestResponse> GetToken(AuthModel authModel)
        {
            var client = new RestClient(_url);
            var request = new RestRequest(_loginPath, Method.Post);
            request.AddJsonBody(authModel);
            var response = client.PostAsync(request);

            return response;
        }

        public Task<RestResponse> RegistrateUser(LeadModel leadModel)
        {
            var client = new RestClient(_url);
            var request = new RestRequest("", Method.Post);
            request.AddJsonBody(leadModel);
            var response = client.PostAsync(request);

            return response;
        }
    }
}
