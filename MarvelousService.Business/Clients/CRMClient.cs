using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Models.CRMModels;
using RestSharp;
using RestSharp.Authenticators;

namespace MarvelousService.BusinessLayer.Services
{
    public class CRMClient : ICRMClient
    {
        private RestClient _client;
        private readonly IHelper _helper;
        private const string _url = "https://piter-education.ru:5050";
        private const string _loginPath = "/api/auth/login/";
        private const string _addLeadPath = "/api/leads/";
        private const string _getAccountByLeadIdPath = "/api/accounts/";

        public CRMClient(IHelper helper)
        {
            _client = new RestClient(_url);
            _helper = helper;
        }

        public async Task Authorize(AuthModel authModel)
        {
            var authRequest = new RestRequest(_loginPath, Method.Post)
                .AddHeader("Accept", "text/plain")
                .AddHeader("Content-Type", "application/json")
                .AddJsonBody(authModel);

            var response = await _client.ExecuteAsync<string>(authRequest);
            _helper.CheckMicroserviceResponse(response);
            _client.Authenticator = new JwtAuthenticator(response.Data);
        }

        public async Task<List<AccountModel>> GetLeadAccounts()
        {
            var request = new RestRequest(_getAccountByLeadIdPath, Method.Get);
            var response = await _client.ExecuteAsync<List<AccountModel>>(request);
            _helper.CheckMicroserviceResponse(response);
            return response.Data;
        }

        public async Task<int> AddLead(LeadModel lead)
        {
            var request = new RestRequest(_addLeadPath, Method.Post);
            request.AddJsonBody(lead);
            var response = await _client.PostAsync(request);
            _helper.CheckMicroserviceResponse(response);
            return Convert.ToInt32(response.Content);
        }

        public async Task<string> GetToken(AuthModel authModel)
        {
            var request = new RestRequest(_loginPath, Method.Post);
            request.AddJsonBody(authModel);
            var response = await _client.PostAsync(request);
            _helper.CheckMicroserviceResponse(response);
            return response.Content;
        }
    }
}
