using MarvelousService.BusinessLayer.Models;
using MarvelousService.DataLayer.Entities;

namespace MarvelousService.BusinessLayer.Services.Interfaces
{
    public interface IServiceService
    {
        int AddService(ServiceModel serviceModel);
        void SoftDeleted(int id, ServiceModel serviceModel);
        void UpdateService(int id, ServiceModel serviceModel);
        ServiceModel GetServiceById(int id);
    }
}
