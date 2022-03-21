using MarvelousService.BusinessLayer.Models;

namespace MarvelousService.BusinessLayer.Services.Interfaces
{
    public interface IServiceToService
    {
        Task<long> AddService(ServiceModel serviceModel);
        Task SoftDelete(long id, ServiceModel serviceModel);
        Task UpdateService(long id, ServiceModel serviceModel);
        Task<ServiceModel> GetServiceById(long id);
        Task<ServicePaymentModel> GetTransactionByServiceToLeadId(long id);
    }
}
