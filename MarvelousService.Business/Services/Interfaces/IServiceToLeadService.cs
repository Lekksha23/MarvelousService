using MarvelousService.BusinessLayer.Models;


namespace MarvelousService.BusinessLayer.Services.Interfaces
{
    public interface IServiceToLeadService
    {
        Task<List<ServiceToLeadModel>>GetServiceToLeadById(int id);
        Task<int> AddServiceToLead(ServiceToLeadModel serviceModel, int role);
        Task<List<ServiceToLeadModel>> GetLeadById(int id);
    }
}
