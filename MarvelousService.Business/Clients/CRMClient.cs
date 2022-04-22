using Marvelous.Contracts.Autentificator;
using Marvelous.Contracts.Endpoints;
using Marvelous.Contracts.Enums;
using MarvelousService.BusinessLayer.Helpers;
using MarvelousService.BusinessLayer.Models;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace MarvelousService.BusinessLayer.Clients
{
    public class CRMClient : ICRMClient
    {
        private RestClient _client;
        private readonly IRequestHelper _requestHelper;
        private readonly IConfiguration _config;
        private const string _getAccountByLeadIdPath = "/api/accounts/";

        public CRMClient(IRequestHelper requestHelper, IConfiguration config)
        {
            _requestHelper = requestHelper;
            _config = config;
            _client = new RestClient();
            _client.AddDefaultHeader(nameof(Microservice), Microservice.MarvelousResource.ToString());
        }

        public async Task<List<AccountModel>> GetLeadAccounts(string jwtToken)
        {
            _client.Authenticator = new MarvelousAuthenticator(jwtToken);
            var request = new RestRequest(_getAccountByLeadIdPath, Method.Get);
            var response = await _client.ExecuteAsync<List<AccountModel>>(request);
            _requestHelper.CheckMicroserviceResponse(response);
            return response.Data;
        }

        public async Task<int> AddLead(LeadModel lead)
        {
            var request = new RestRequest(CrmEndpoints.LeadApi, Method.Post);
            request.AddJsonBody(lead);
            var response = await _client.PostAsync(request);
            _requestHelper.CheckMicroserviceResponse(response);
            return Convert.ToInt32(response.Content);
        }
    }
}
