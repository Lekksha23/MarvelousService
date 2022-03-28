using MarvelousService.BusinessLayer.Models;

namespace MarvelousService.BusinessLayer.Services.Interfaces
{
    public interface ICRMClient
    {
        Task<string> GetToken(AuthModel authModel);
    }
}
