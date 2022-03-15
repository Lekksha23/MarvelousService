using MarvelousService.BusinessLayer.Models;
using MarvelousService.DataLayer.Entities;

namespace MarvelousService.BusinessLayer.Services.Interfaces
{
    public interface IServiceService
    {
        int AddService(ServiceModel serviceModel);
        Service SoftDeleted(ServiceModel serviceModel);
        int UpdateService(ServiceModel serviceModel);
        ServiceModel GetServiceById(int id);
    }
}
