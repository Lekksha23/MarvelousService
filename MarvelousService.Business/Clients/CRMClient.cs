using MarvelousService.BusinessLayer.Exceptions;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Models.CRMModels;
using MarvelousService.BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.Logging;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;

namespace MarvelousService.BusinessLayer.Services
{
    public class CRMClient : ICRMClient
    {
        private RestClient _client;
        private readonly ILogger<CRMClient> _logger;
        private const string _url = "https://piter-education.ru:5050";
        private const string _loginPath = "/api/auth/login/";
        private const string _addLeadPath = "/api/leads/";
        private const string _getAccountByLeadIdPath = "/api/accounts/";

        public CRMClient(ILogger<CRMClient> logger)
        {
            _client = new RestClient(_url);
            _logger = logger;
        }

        public async Task Authorize(AuthModel authModel)
        {
            var authRequest = new RestRequest(_loginPath, Method.Post)
                .AddHeader("Accept", "text/plain")
                .AddHeader("Content-Type", "application/json")
                .AddJsonBody(authModel);

            var response = await _client.ExecuteAsync<string>(authRequest);
            CheckCRMResponse(response);
            _client.Authenticator = new JwtAuthenticator(response.Data);
        }

        public async Task<List<AccountModel>> GetLeadAccounts()
        {
            var request = new RestRequest(_getAccountByLeadIdPath, Method.Get);
            var response = await _client.ExecuteAsync<List<AccountModel>>(request);
            CheckCRMResponse(response);
            return response.Data;
        }

        public async Task<int> AddLead(LeadModel lead)
        {
            var request = new RestRequest(_addLeadPath, Method.Post);
            request.AddJsonBody(lead);
            var response = await _client.PostAsync(request);
            CheckCRMResponse(response);
            return Convert.ToInt32(response.Content);
        }

        public async Task<string> GetToken(AuthModel authModel)
        {
            var request = new RestRequest(_loginPath, Method.Post);
            request.AddJsonBody(authModel);
            var response = await _client.PostAsync(request);
            CheckCRMResponse(response);
            return response.Content;
        }

        private void CheckCRMResponse(RestResponse response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.RequestTimeout:
                    _logger.LogError($"Request Timeout {response.ErrorException.Message}");
                    throw new RequestTimeoutException(response.ErrorException.Message);
                    break;
                case HttpStatusCode.ServiceUnavailable:
                    _logger.LogError($"Service Unavailable {response.ErrorException.Message}");
                    throw new ServiceUnavailableException(response.ErrorException.Message);
                    break;
                case HttpStatusCode.BadGateway:
                    _logger.LogError($"Bad gatеway {response.ErrorException.Message}");
                    throw new BadGatewayException(response.ErrorException.Message);
                    break;
                case HttpStatusCode.BadRequest:
                    _logger.LogError($"Bad request {response.ErrorException.Message}");
                    throw new BadRequestException(response.ErrorException.Message);
                    break;
                case HttpStatusCode.Unauthorized:
                    _logger.LogError($"Unauthorized {response.ErrorException.Message}");
                    throw new BadRequestException(response.ErrorException.Message);
                    break;
            }

            if (response.Content == null)
            {
                _logger.LogError($"CRM response content equals null!");
                throw new BadGatewayException("CRM response content equals null!");
            }
            if (response.StatusCode != HttpStatusCode.OK)
            {
                _logger.LogError($"Incorrect response from CRM {response.ErrorException.Message}");
                throw new InternalServerError(response.ErrorException.Message);
            }
        }
    }
}
