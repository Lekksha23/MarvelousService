using MarvelousService.DataLayer.Entities;

namespace MarvelousService.DataLayer.Repositories.Interfaces
{
    public interface IServiceRepository
    {
        Task<int> AddService(Service service);
        void SoftDelete(int id,Service service);
        void UpdateService(int id, Service service);
        Task<Service> GetServiceById(int id);
    }
}
