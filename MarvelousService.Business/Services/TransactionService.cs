using Marvelous.Contracts.Enums;
using Marvelous.Contracts.RequestModels;
using Microsoft.Extensions.Logging;

namespace MarvelousService.BusinessLayer.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionStoreClient _transactionClient;
        private readonly ILogger<TransactionService> _logger;

        public TransactionService(ITransactionStoreClient transactionClient, ILogger<TransactionService> logger)
        {
            _transactionClient = transactionClient;
            _logger = logger;
        }

        public async Task<long> AddResourceTransaction(int accountId, decimal totalPrice)
        {
            _logger.LogInformation("Query for adding a resource transaction in TransactionStore");
            var resourceTransaction = new TransactionRequestModel
            {
                Amount = totalPrice,
                Currency = Currency.RUB,
                AccountId = accountId
            };
            return await _transactionClient.AddResourceTransaction(resourceTransaction);
        }
    }
}
