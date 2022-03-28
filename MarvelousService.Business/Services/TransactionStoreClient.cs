﻿using RestSharp;
using Marvelous.Contracts;
using Microsoft.Extensions.Logging;

namespace MarvelousService.BusinessLayer.Services
{
    public class TransactionStoreClient : ITransactionStoreClient
    {
        private const string _url = "https://piter-education.ru:6060";
        private const string _transactionPath = "/api/service-payment/";
        private readonly ILogger<ServiceToService> _logger;

        public TransactionStoreClient(ILogger<ServiceToService> logger)
        {
            _logger = logger;
        }

        public async Task<int> AddTransaction(TransactionRequestModel transactionRequestModel)
        {
            var client = new RestClient(_url);
            var request = new RestRequest(_transactionPath, Method.Post);
            request.AddJsonBody(transactionRequestModel);
            _logger.LogInformation("Запрос на добавление транзакции для оплаты услуги");
            var response = await client.PostAsync<int>(request);
            return response;
        }
    }
}
