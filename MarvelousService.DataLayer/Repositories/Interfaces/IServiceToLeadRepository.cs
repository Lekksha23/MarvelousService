using MarvelousService.DataLayer.Entities;

namespace MarvelousService.DataLayer.Interfaces
{
    public interface IServiceToLeadRepository
    {
        Task<ServiceToLead> GetServiceToLeadById(long id);
        Task<long> AddServiceToLead(ServiceToLead service);
        Task<List<ServiceToLead>> GetByLeadId(long id);
    }
}
