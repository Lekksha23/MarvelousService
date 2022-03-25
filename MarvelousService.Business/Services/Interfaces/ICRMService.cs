using MarvelousService.BusinessLayer.Models;

namespace MarvelousService.BusinessLayer.Services.Interfaces
{
    public interface ICRMService
    {
        Task<string> GetToken(AuthModel authModel);
    }
}
