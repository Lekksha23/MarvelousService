
namespace MarvelousService.BusinessLayer.Services
{
    public interface ITransactionService
    {
        Task<long> AddResourceTransaction(int accountId, decimal price);
    }
}