using MarvelousService.BusinessLayer.Models;

namespace MarvelousService.BusinessLayer.Services.Interfaces
{
    public interface IAuthService
    {
        string GetToken(AuthModel authModel);
    }
}
