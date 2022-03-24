using MarvelousService.BusinessLayer.Models;
using MarvelousService.DataLayer.Entities;

namespace MarvelousService.BusinessLayer.Services
{
    public interface IServicePaymentService
    {
        Task<int> AddServicePayment(ServicePayment servicePaymentModel);
        Task<List<ServicePaymentModel>> GetServiceById(int serviceToLeadId);
    }
}