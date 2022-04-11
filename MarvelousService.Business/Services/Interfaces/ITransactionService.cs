
namespace MarvelousService.BusinessLayer.Clients
{
    public interface ITransactionService
    {
        Task<long> AddResourceTransaction(int accountId, decimal price);
    }
}