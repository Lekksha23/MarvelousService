using MarvelousService.BusinessLayer.Models;

namespace MarvelousService.BusinessLayer.Services.Interfaces
{
    public interface IServiceService
    {
        int AddService(ServiceModel serviceModel);
        ServiceModel SoftDeleted(ServiceModel serviceModel);
        ServiceModel UpdateService(ServiceModel serviceModel);
        ServiceModel GetServiceById(int id);
    }
}
