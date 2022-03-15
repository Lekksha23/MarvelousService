using MarvelousService.BusinessLayer.Models;

namespace MarvelousService.BusinessLayer.Services.Interfaces
{
    public interface IServiceService
    {
        int AddService(ServiceModel serviceModel);
        int SoftDeleted(ServiceModel serviceModel);
        ServiceModel UpdateService(ServiceModel serviceModel);
        ServiceModel GetServiceById(int id);
    }
}
