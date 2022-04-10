using Marvelous.Contracts.Enums;
using MarvelousService.BusinessLayer.Models;


namespace MarvelousService.BusinessLayer.Services.Interfaces
{
    public interface ILeadResourceService
    {
        Task<int> AddLeadResource(LeadResourceModel serviceModel, Role role);
        Task<List<LeadResourceModel>>GetById(int id);
        Task<List<LeadResourceModel>> GetByLeadId(int id);
        Task<List<LeadResourceModel>> GetByPayDate(DateTime payDate);
    }
}
