
namespace MarvelousService.BusinessLayer.Clients
{
    public interface ICRMService
    {
        Task<int> GetIdOfRubLeadAccount(string jwtToken);
    }
}