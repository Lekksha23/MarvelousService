using MarvelousService.BusinessLayer.Models;


namespace MarvelousService.BusinessLayer.Services.Interfaces
{
    public interface IServiceToLeadService
    {
        Task<ServiceToLeadModel> GetServiceToLeadById(int id);
        Task<int> AddServiceToLead(ServiceToLeadModel serviceModel);
        Task<List<ServiceToLeadModel>> GetLeadById(int id);
    }
}
