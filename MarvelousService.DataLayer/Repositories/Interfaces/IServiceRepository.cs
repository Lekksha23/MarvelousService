using MarvelousService.DataLayer.Entities;

namespace MarvelousService.DataLayer.Repositories.Interfaces
{
    public interface IServiceRepository
    {
        int AddService(Service service);
        void SoftDelete(Service service);
        void UpdateService(Service service);
        Service GetServiceById(int id);
    }
}
