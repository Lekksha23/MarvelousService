using MarvelousService.DataLayer.Entities;

namespace MarvelousService.DataLayer.Repositories
{
    public interface IServicePaymentRepository
    {
        Task<int> AddServicePayment(ServicePayment servicePayment);
        Task<List<ServicePayment>> GetServicePaymentsByServiceToLeadId(int id);
    }
}