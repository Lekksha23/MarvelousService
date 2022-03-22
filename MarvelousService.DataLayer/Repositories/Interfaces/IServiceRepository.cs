using MarvelousService.DataLayer.Entities;

namespace MarvelousService.DataLayer.Repositories.Interfaces
{
    public interface IServiceRepository
    {
        Task<int> AddService(Service service);
        Task SoftDelete(int id,Service service);
        Task UpdateService(int id, Service service);
        Task<Service> GetServiceById(int id);
    }
} 
