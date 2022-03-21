using MarvelousService.BusinessLayer.Models;

namespace MarvelousService.BusinessLayer.Services.Interfaces
{
    public interface IServiceToService
    {
        Task<int> AddService(ServiceModel serviceModel);
        void SoftDelete(int id, ServiceModel serviceModel);
        void UpdateService(int id, ServiceModel serviceModel);
        Task<ServiceModel> GetServiceById(int id);
    }
}
