using Marvelous.Contracts.RequestModels;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace MarvelousService.BusinessLayer.Services
{
    public class TransactionStoreClient : ITransactionStoreClient
    {
        private readonly IHelper _helper;
        private const string _url = "https://piter-education.ru:6060";
        private const string _transactionPath = "/api/service-payment/";
        private readonly ILogger<ResourceService> _logger;

        public TransactionStoreClient(IHelper helper)
        {
            _helper = helper;
        }

        public async Task<long> AddResourceTransaction(TransactionRequestModel transactionRequestModel)
        {
            var client = new RestClient(_url);
            var request = new RestRequest(_transactionPath, Method.Post);
            request.AddJsonBody(transactionRequestModel);
            var response = await client.PostAsync(request);
            _helper.CheckMicroserviceResponse(response);
            return Convert.ToInt64(response.Content);
        }
    }
}
