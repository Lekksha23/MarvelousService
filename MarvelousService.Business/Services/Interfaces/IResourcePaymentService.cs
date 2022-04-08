using MarvelousService.BusinessLayer.Models;

namespace MarvelousService.BusinessLayer.Services
{
    public interface IResourcePaymentService
    {
        Task<List<ResourcePaymentModel>> GetResourcePaymentsById(int leadResourceId);
    }
}