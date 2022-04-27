using MarvelousService.BusinessLayer.Models;

namespace MarvelousService.BusinessLayer.Clients
{
    public interface IResourcePaymentService
    {
        Task<List<ResourcePaymentModel>> GetResourcePaymentsById(int leadResourceId);
    }
}