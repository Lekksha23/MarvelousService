using MarvelousService.BusinessLayer.Models;


namespace MarvelousService.BusinessLayer.Services.Interfaces
{
    public interface ILeadResourceService
    {
        Task<List<LeadResourceModel>>GetLeadResourceById(int id);
        Task<int> AddLeadResource(LeadResourceModel serviceModel, int role);
        Task<List<LeadResourceModel>> GetByLeadId(int id);
    }
}
