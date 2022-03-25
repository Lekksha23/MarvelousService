using MarvelousService.BusinessLayer.Models;

namespace MarvelousService.BusinessLayer.Services.Interfaces
{
    public interface IServiceToService
    {
        Task<int> AddService(ServiceModel serviceModel, int role);
        Task SoftDelete(int id, ServiceModel serviceModel);
        Task UpdateService(int id, ServiceModel serviceModel);
        Task<ServiceModel> GetServiceById(int id);
    }
}
