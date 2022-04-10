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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MarvelousService.BusinessLayer.Helpers
{
    public class ReqvestHelper : IReqvestHelper
    {
        private readonly ILogger<ReqvestHelper> _logger;
        private readonly IConfiguration _config;

        public ReqvestHelper(ILogger<ReqvestHelper> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }


        private static void CheckTransactionError<T>(RestResponse<T> response)
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
                    throw new BadGatewayException($"Error  {response.ErrorException!.Message}");
            }
            if (response.Data is null)
                throw new BadGatewayException($"Content equal's null {response.ErrorException!.Message}");
        }
        public async Task<RestResponse<T>> SendRequest<T>(string path, Microservice service, string jwtToken = "null")
        {

            var request = new RestRequest(path);
            var client = new RestClient(_config[service.ToString()]);
            client.Authenticator = new JwtAuthenticator(jwtToken);
            client.AddDefaultHeader(nameof(Microservice), Microservice.MarvelousResource.ToString());
            var response = await client.ExecuteAsync<T>(request);
            CheckTransactionError(response);
            return response;
        }


        public async Task<RestResponse<T>> GetTokenFromFront<T>( Microservice service, AuthRequestModel authReguest)
        {

            var request = new RestRequest(AuthEndpoints.ApiAuth + AuthEndpoints.Login);
            var client = new RestClient(_config[service.ToString()]);
            //client.Authenticator = new JwtAuthenticator(jwtToken);
            client.AddDefaultHeader(nameof(Microservice), Microservice.MarvelousResource.ToString());
            var response = await client.ExecuteAsync<T>(request);
            CheckTransactionError(response);
            return response;
        }

        public async Task<RestResponse<IdentityResponseModel>> SendRequestCheckValidateToken(string jwtToken) // Когда Токен есть
        {
            var request = new RestRequest(AuthEndpoints.ApiAuth + AuthEndpoints.ValidationFront);
            var client = new RestClient(_config[Microservice.MarvelousAuth.ToString()]);
            client.Authenticator = new MarvelousAuthenticator(jwtToken);
            client.AddDefaultHeader(nameof(Microservice), Microservice.MarvelousTransactionStore.ToString());
            var response = await client.ExecuteAsync<IdentityResponseModel>(request);
            CheckTransactionError(response);

            return response;
        }

        

    }
}
