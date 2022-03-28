using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Enums;

namespace MarvelousService.DataLayer.Repositories
{
    public interface IServiceToLeadRepository
    {
        Task<int> AddServiceToLead(ServiceToLead serviceToLead);
        Task<List<ServiceToLead>> GetByLeadId(int id);
        Task<ServiceToLead> GetServiceToLeadById(int id);
        void UpdateStatusById(int id, Status status);
    }
}