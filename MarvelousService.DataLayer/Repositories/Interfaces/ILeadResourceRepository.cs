using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Enums;

namespace MarvelousService.DataLayer.Repositories
{
    public interface ILeadResourceRepository
    {
        Task<int> AddLeadResource(LeadResource serviceToLead);
        Task<List<LeadResource>> GetByLeadId(int id);
        Task<List<LeadResource>> GetByPayDate(DateTime payDate);
        Task<LeadResource> GetLeadResourceById(int id);
        void UpdateStatusById(int id, Status status);
    }
}