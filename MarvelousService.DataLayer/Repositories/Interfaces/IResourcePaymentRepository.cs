using MarvelousService.DataLayer.Entities;

namespace MarvelousService.DataLayer.Repositories
{
    public interface IResourcePaymentRepository
    {
        Task<int> AddResourcePayment(ResourcePayment resourcePayment);
        Task<List<ResourcePayment>> GetResourcePaymentsByLeadResourceId(int id);
    }
}