using Marvelous.Contracts.Endpoints;
using Marvelous.Contracts.Enums;
using Marvelous.Contracts.ResponseModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MarvelousService.BusinessLayer.Helpers
{
    public class InitializeHelper : IInitializeHelper
    {
        private readonly ILogger<InitializeHelper> _logger;
        private readonly IRequestHelper _requestHelper;
        private readonly IConfiguration _config;

        public InitializeHelper(ILogger<InitializeHelper> logger, IRequestHelper reqvestHelper, IConfiguration config)
        {
            _logger = logger;
            _requestHelper = reqvestHelper;
            _config = config;
        }

        public async Task InitializeConfig()
        {
            _logger.LogInformation("Query for getting a token from IdentityMicroservice for communication between microservices.");
            var token = await _requestHelper
                .SendRequest<string>(AuthEndpoints.ApiAuth + AuthEndpoints.TokenForMicroservice, Microservice.MarvelousAuth); // Получили от IdentityService токен для общения между Микросервисами.
            _logger.LogInformation("Token has been received. Query for initialization of configurations started.");
            var configData = await _requestHelper
                .SendRequest<IEnumerable<ConfigResponseModel>>(ConfigsEndpoints.Configs, Microservice.MarvelousConfigs, token.Data); // Получили от настроек конфигурации.
            _logger.LogInformation("Configurations have been received.");
            foreach (var e in configData.Data)
            {
                // Key название Микросервиса. Value нужные нам url
                _config[e.Key] = e.Value;
            }
        }
    }
}
