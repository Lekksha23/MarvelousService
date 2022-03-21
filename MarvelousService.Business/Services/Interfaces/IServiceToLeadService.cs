using MarvelousService.BusinessLayer.Models;


namespace MarvelousService.BusinessLayer.Services.Interfaces
{
    public interface IServiceToLeadService
    {
        Task<ServiceToLeadModel> GetServiceToLeadById(long id);
        Task<long> AddServiceToLead(ServiceToLeadModel serviceModel);
        Task<List<ServiceToLeadModel>> GetLeadById(long id);
    }
}
