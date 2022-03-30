using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Models.CRMModels;
using MarvelousService.BusinessLayer.Services.Interfaces;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;

namespace MarvelousService.BusinessLayer.Services
{
    public class CRMClient : ICRMClient
    {
        private RestClient _client;
        private const string _url = "https://piter-education.ru:5050";
        private const string _loginPath = "/api/auth/login/";
        private const string _addLeadPath = "/api/leads/";
        private const string _getAccountByLeadIdPath = "/api/accounts/";

        public CRMClient()
        {
            _client = new RestClient(_url);
        }

        public async Task Authorize(AuthModel authModel)
        {
            var authRequest = new RestRequest(_loginPath, Method.Post)
                .AddHeader("Accept", "text/plain")
                .AddHeader("Content-Type", "application/json")
                .AddJsonBody(authModel);

            var auth = await _client.ExecuteAsync<string>(authRequest);
            if (auth.StatusCode != HttpStatusCode.OK)
            {
                throw new BadGatewayException();
            }
            _client.Authenticator = new JwtAuthenticator(auth.Data);
        }

        public async Task<List<AccountModel>> GetAccountsByLeadId()
        {
            var request = new RestRequest(_getAccountByLeadIdPath, Method.Get);
            var accounts = await _client.ExecuteAsync<List<AccountModel>>(request);
            return accounts.Data;
        }

        public async Task<int> AddLead(LeadModel lead)
        {
            var request = new RestRequest(_addLeadPath, Method.Post);
            request.AddJsonBody(lead);
            return await _client.PostAsync<int>(request);
        }

        public async Task<string> GetToken(AuthModel authModel)
        {
            var request = new RestRequest(_loginPath, Method.Post);
            request.AddJsonBody(authModel);
            var response = await _client.PostAsync<string>(request);
            return response;
        }
    }
}
