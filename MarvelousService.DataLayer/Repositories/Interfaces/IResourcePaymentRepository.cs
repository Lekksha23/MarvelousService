using MarvelousService.DataLayer.Entities;

namespace MarvelousService.DataLayer.Repositories
{
    public interface IResourcePaymentRepository
    {
        Task<int> AddResourcePayment(LeadResource leadResource, long transactionId);
        Task<List<ResourcePayment>> GetResourcePaymentsByLeadResourceId(int id);
    }
}