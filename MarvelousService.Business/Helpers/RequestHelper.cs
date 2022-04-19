using Marvelous.Contracts.Autentificator;
using Marvelous.Contracts.Endpoints;
using Marvelous.Contracts.Enums;
using Marvelous.Contracts.RequestModels;
using Marvelous.Contracts.ResponseModels;
using MarvelousService.BusinessLayer.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;

namespace MarvelousService.BusinessLayer.Helpers
{
    public class RequestHelper : IRequestHelper
    {
        private readonly ILogger<RequestHelper> _logger;
        private readonly IConfiguration _config;

        public RequestHelper(ILogger<RequestHelper> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public async Task<RestResponse<T>> SendRequest<T>(string path, Microservice service, string jwtToken = "null")
        {
            var request = new RestRequest(path);
            var client = new RestClient(_config[service.ToString()]);
            client.Authenticator = new JwtAuthenticator(jwtToken);
            client.AddDefaultHeader(nameof(Microservice), Microservice.MarvelousResource.ToString());
            var response = await client.ExecuteAsync<T>(request);
            CheckMicroserviceResponse(response);
            return response;
        }

        public async Task<RestResponse<T>> GetTokenForFront<T>( Microservice service, AuthRequestModel authRequest)
        {
            var request = new RestRequest(AuthEndpoints.ApiAuth + AuthEndpoints.Login, Method.Post);
            var client = new RestClient(_config[service.ToString()]);
            client.AddDefaultHeader(nameof(Microservice), Microservice.MarvelousResource.ToString());
            request.AddBody(authRequest);
            var response = await client.ExecuteAsync<T>(request);
            CheckMicroserviceResponse(response);
            return response;
        }

        public async Task<IdentityResponseModel>SendRequestToValidateToken(string jwtToken)
        {
            var request = new RestRequest(AuthEndpoints.ApiAuth + AuthEndpoints.ValidationFront);
            var client = new RestClient(_config[Microservice.MarvelousAuth.ToString()]);
            client.Authenticator = new MarvelousAuthenticator(jwtToken);
            client.AddDefaultHeader(nameof(Microservice), Microservice.MarvelousResource.ToString());
            var response = await client.ExecuteAsync<IdentityResponseModel>(request);
            CheckMicroserviceResponse(response);
            return response.Data;
        }

        public void CheckMicroserviceResponse(RestResponse response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    break;
                case HttpStatusCode.RequestTimeout:
                    throw new RequestTimeoutException($"Request Timeout {response.ErrorException!.Message}");
                case HttpStatusCode.ServiceUnavailable:
                    throw new ServiceUnavailableException($"Service Unavailable {response.ErrorException!.Message}");
                default:
                    throw new BadGatewayException($"Error {response.ErrorException!.Message}");
            }
            if (response.Content == null)
                throw new BadGatewayException($"Content equal's null {response.ErrorException!.Message}");
        }
    }
}
