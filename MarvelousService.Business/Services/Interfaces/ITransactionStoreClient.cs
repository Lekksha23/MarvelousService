using Marvelous.Contracts;

namespace MarvelousService.BusinessLayer.Services
{
    public interface ITransactionStoreClient
    {
        Task<int> AddTransaction(TransactionRequestModel transactionRequestModel);
    }
}