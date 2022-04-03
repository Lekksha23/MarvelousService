using MarvelousService.BusinessLayer.Models;
using MarvelousService.DataLayer.Entities;

namespace MarvelousService.BusinessLayer.Services
{
    public interface IResourcePaymentService
    {
        Task<int> AddResourcePayment(ResourcePaymentModel servicePaymentModel);
        Task<List<ResourcePaymentModel>> GetResourcePaymentsById(int serviceToLeadId);
    }
}