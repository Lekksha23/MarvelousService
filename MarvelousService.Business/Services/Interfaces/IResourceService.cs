using MarvelousService.BusinessLayer.Models;

namespace MarvelousService.BusinessLayer.Services.Interfaces
{
    public interface IResourceService
    {
        Task<int> AddResource(ResourceModel resourceModel);
        Task SoftDelete(int id, ResourceModel resourceModel);
        Task UpdateResource(int id, ResourceModel resourceModel);
        Task<ResourceModel> GetResourceById(int id);
        Task<List<ResourceModel>> GetAllResources();
    }
}
