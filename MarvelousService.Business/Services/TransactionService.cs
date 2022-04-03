using Marvelous.Contracts.Enums;
using Marvelous.Contracts.RequestModels;

namespace MarvelousService.BusinessLayer.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionStoreClient _transactionClient;

        public TransactionService(ITransactionStoreClient transactionClient)
        {
            _transactionClient = transactionClient;
        }

        public async Task<long> AddResourceTransaction(int accountId, decimal price)
        {
            var resourceTransaction = new TransactionRequestModel
            {
                Amount = price,
                Currency = Currency.RUB,
                AccountId = accountId
            };
            return await _transactionClient.AddResourceTransaction(resourceTransaction);
        }
    }
}
