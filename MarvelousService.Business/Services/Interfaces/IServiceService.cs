using MarvelousService.BusinessLayer.Models;

namespace MarvelousService.BusinessLayer.Services.Interfaces
{
    public interface IServiceService
    {
        public ServiceModel GetServiceById(int id);
        int AddService(ServiceModel serviceModel);
        List<ServiceModel> GetByLeadId(int id);
    }
}
