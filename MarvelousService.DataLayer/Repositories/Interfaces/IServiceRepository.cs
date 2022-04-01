using MarvelousService.DataLayer.Entities;

namespace MarvelousService.DataLayer.Repositories.Interfaces
{
    public interface IServiceRepository
    {
        Task<int> AddService(Service service);
        Task SoftDelete(Service service);
        Task UpdateService(Service service);
        Task<Service> GetServiceById(int id);
        Task<List<Service>> GetAllService();
    }
} 
