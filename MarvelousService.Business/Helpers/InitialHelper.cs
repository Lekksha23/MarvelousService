using Marvelous.Contracts.Endpoints;
using Marvelous.Contracts.Enums;
using Marvelous.Contracts.ResponseModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelousService.BusinessLayer.Helpers
{
    public class InitialHelper : IInitialHelper
    {
        private readonly ILogger<InitialHelper> _logger;

        private readonly IReqvestHelper _reqvestHelper;

        private readonly IConfiguration _config;

        public InitialHelper(ILogger<InitialHelper> logger, IReqvestHelper reqvestHelper, IConfiguration config)
        {
            _logger = logger;

            _reqvestHelper = reqvestHelper;
            _config = config;
        }

        public async Task InitialazeConfig()
        {
            var token = await _reqvestHelper.SendRequest<string>(AuthEndpoints.ApiAuth + AuthEndpoints.TokenForMicroservice, Microservice.MarvelousAuth); // Получили от Степы токен для общения между Микросервисами.

            var confData = await _reqvestHelper.SendRequest<IEnumerable<ConfigResponseModel>>(ConfigsEndpoints.Configs, Microservice.MarvelousConfigs, token.Data); // Получили от настроек конфиурации...

            foreach (var e in confData.Data)
            {
                _config[e.Key] = e.Value;   //Key название Микросервиса... Value наша url
            }

        }



    }
}
