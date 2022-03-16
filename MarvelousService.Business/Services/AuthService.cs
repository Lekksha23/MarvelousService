using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Services.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace MarvelousService.BusinessLayer.Services
{
    public class AuthService : IAuthService
    {
        private const string _url = "https://piter-education.ru:5050";
        private const string _loginPath = "/api/auth/login/";
        private const string _addLeadPath = "/api/leads/";

        public async Task<string> GetToken(AuthModel authModel)
        {
            var client = new RestClient(_url);
            var request = new RestRequest(_loginPath, Method.Post);
            request.AddJsonBody(authModel);
            var response = await client.PostAsync<string>(request);
            //var result = JsonConvert.DeserializeObject<dynamic>(response.Content);
            //var token = result.access_token;

            return response;
        }

        public async Task<RestResponse> RegistrateLead(LeadModel leadModel)
        {
            var client = new RestClient(_url);
            var request = new RestRequest(_addLeadPath, Method.Post);
            request.AddJsonBody(leadModel);
            var response = await client.PostAsync(request);

            return response;
        }
    }
}
