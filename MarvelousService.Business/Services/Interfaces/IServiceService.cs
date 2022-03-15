using MarvelousService.BusinessLayer.Models;
using MarvelousService.DataLayer.Entities;

namespace MarvelousService.BusinessLayer.Services.Interfaces
{
    public interface IServiceService
    {
        int AddService(ServiceModel serviceModel);
        void SoftDelete(ServiceModel serviceModel);
        void UpdateService(ServiceModel serviceModel);
        ServiceModel GetServiceById(int id);
    }
}
