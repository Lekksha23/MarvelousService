using Marvelous.Contracts;

namespace MarvelousService.BusinessLayer.Services
{
    public interface ITransactionService
    {
        Task<int> AddTransaction(TransactionRequestModel transactionRequestModel);
    }
}