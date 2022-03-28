using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Services.Interfaces;
using RestSharp;

namespace MarvelousService.BusinessLayer.Services
{
    public class CRMClient : ICRMClient
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
            return response;
        }

    }
}
