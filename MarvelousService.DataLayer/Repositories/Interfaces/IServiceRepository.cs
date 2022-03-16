using MarvelousService.DataLayer.Entities;

namespace MarvelousService.DataLayer.Repositories.Interfaces
{
    public interface IServiceRepository
    {
        int AddService(Service service);
        void SoftDeleted(int id,Service service);
        void UpdateService(int id, Service service);
        Service GetServiceById(int id);
    }
}
