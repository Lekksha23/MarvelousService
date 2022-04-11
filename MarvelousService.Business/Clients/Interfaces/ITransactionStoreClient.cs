using Marvelous.Contracts.RequestModels;

namespace MarvelousService.BusinessLayer.Clients
{
    public interface ITransactionStoreClient
    {
        Task<long> AddResourceTransaction(TransactionRequestModel transactionRequestModel);
    }
}