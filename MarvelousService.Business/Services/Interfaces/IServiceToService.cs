using MarvelousService.BusinessLayer.Models;

namespace MarvelousService.BusinessLayer.Services.Interfaces
{
    public interface IServiceToService
    {
        int AddService(ServiceModel serviceModel);
        void SoftDelete(int id, ServiceModel serviceModel);
        void UpdateService(int id, ServiceModel serviceModel);
        ServiceModel GetServiceById(int id);
    }
}
