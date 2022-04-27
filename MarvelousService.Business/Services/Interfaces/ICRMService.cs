
namespace MarvelousService.BusinessLayer.Clients
{
    public interface ICrmService
    {
        Task<int> GetIdOfRubLeadAccount(string jwtToken);
    }
}