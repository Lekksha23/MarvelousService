using MarvelousService.BusinessLayer.Models;


namespace MarvelousService.BusinessLayer.Services.Interfaces
{
    public interface IServiceToLeadService
    {
        public ServiceToLeadModel GetServiceToLeadById(int id);
        int AddServiceToLead(ServiceToLeadModel serviceModel);
        List<ServiceToLeadModel> GetLeadById(int id);
    }
}
