using MarvelousService.DataLayer.Entities;

namespace MarvelousService.DataLayer.Repositories.Interfaces
{
    public interface IResourceRepository
    {
        Task<int> AddResource(Resource resource);
        Task SoftDelete(Resource resource);
        Task UpdateResource(Resource resource);
        Task<Resource> GetResourceById(int id);
        Task<List<Resource>> GetAllResources();
    }
} 
