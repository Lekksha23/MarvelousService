using MarvelousService.BusinessLayer.Models;
using MarvelousService.DataLayer.Entities;

namespace MarvelousService.BusinessLayer.Services
{
    public interface IServicePaymentService
    {
        Task<int> AddServicePayment(ServicePaymentModel servicePaymentModel);
        Task<List<ServicePaymentModel>> GetServicePaymentsById(int serviceToLeadId);
    }
}