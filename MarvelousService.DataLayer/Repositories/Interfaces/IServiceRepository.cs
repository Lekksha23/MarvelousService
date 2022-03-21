using MarvelousService.DataLayer.Entities;

namespace MarvelousService.DataLayer.Repositories.Interfaces
{
    public interface IServiceRepository
    {
        Task<long> AddService(Service service);
        Task SoftDelete(long id,Service service);
        Task UpdateService(long id, Service service);
        Task<Service> GetServiceById(long id);
        Task<ServicePayment> GetTransactionByServiceToleadId(long id);


    }
} 
