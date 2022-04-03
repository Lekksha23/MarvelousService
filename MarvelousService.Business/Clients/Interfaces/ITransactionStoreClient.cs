using Marvelous.Contracts.RequestModels;

namespace MarvelousService.BusinessLayer.Services
{
    public interface ITransactionStoreClient
    {
        Task<long> AddResourceTransaction(TransactionRequestModel transactionRequestModel);
    }
}