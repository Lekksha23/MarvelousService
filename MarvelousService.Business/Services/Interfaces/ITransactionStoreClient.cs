using Marvelous.Contracts;

namespace MarvelousService.BusinessLayer.Services
{
    public interface ITransactionStoreClient
    {
        Task<long> AddTransaction(TransactionRequestModel transactionRequestModel);
    }
}