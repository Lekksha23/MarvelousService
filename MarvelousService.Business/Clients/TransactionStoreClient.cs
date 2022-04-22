using Marvelous.Contracts.Endpoints;
using Marvelous.Contracts.Enums;
using Marvelous.Contracts.RequestModels;
using MarvelousService.BusinessLayer.Helpers;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace MarvelousService.BusinessLayer.Clients
{
    public class TransactionStoreClient : ITransactionStoreClient
    {
        private readonly IRequestHelper _requestHelper;
        private readonly IConfiguration _config;

        public TransactionStoreClient(IRequestHelper requestHelper, IConfiguration config)
        {
            _requestHelper = requestHelper;
            _config = config;
        }

        public async Task<long> AddResourceTransaction(TransactionRequestModel transactionRequestModel)
        {
            var client = new RestClient();
            var request = new RestRequest($"{_config[Microservice.MarvelousTransactionStore.ToString() + "Url"]}{TransactionEndpoints.ApiTransactions + TransactionEndpoints.ServicePayment}", Method.Post);
            request.AddBody(transactionRequestModel);
            var response = await client.PostAsync(request);
            _requestHelper.CheckMicroserviceResponse(response);
            return Convert.ToInt64(response.Content);
        }
    }
}
