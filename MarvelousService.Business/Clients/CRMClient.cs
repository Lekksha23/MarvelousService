using MarvelousService.BusinessLayer.Helpers;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Models.CRMModels;
using RestSharp;

namespace MarvelousService.BusinessLayer.Clients
{
    public class CRMClient : ICRMClient
    {
        private readonly RestClient _client;
        private readonly IRequestHelper _requestHelper;
        private const string _url = "https://piter-education.ru:5050";
        private const string _loginPath = "/api/auth/login/";
        private const string _addLeadPath = "/api/leads/";
        private const string _getAccountByLeadIdPath = "/api/accounts/";

        public CRMClient(IRequestHelper requestHelper)
        {
            _client = new RestClient(_url);
            _requestHelper = requestHelper;
        }

        public async Task<List<AccountModel>> GetLeadAccounts()
        {
            var request = new RestRequest(_getAccountByLeadIdPath, Method.Get);
            var response = await _client.ExecuteAsync<List<AccountModel>>(request);
            _requestHelper.CheckMicroserviceResponse(response);
            return response.Data;
        }

        public async Task<int> AddLead(LeadModel lead)
        {
            var request = new RestRequest(_addLeadPath, Method.Post);
            request.AddJsonBody(lead);
            var response = await _client.PostAsync(request);
            _requestHelper.CheckMicroserviceResponse(response);
            return Convert.ToInt32(response.Content);
        }
    }
}
