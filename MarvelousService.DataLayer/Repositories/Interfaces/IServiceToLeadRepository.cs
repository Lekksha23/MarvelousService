using MarvelousService.DataLayer.Entities;

namespace MarvelousService.DataLayer.Interfaces
{
    public interface IServiceToLeadRepository
    {
        Task<List<ServiceToLead>> GetServiceToLeadById(int id);
        Task<int> AddServiceToLead(ServiceToLead service);
        Task<List<ServiceToLead>> GetByLeadId(int id);
    }
}
