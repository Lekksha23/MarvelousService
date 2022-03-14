using RestSharp;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Services.Interfaces;

namespace CRM.BusinessLayer.Services
{
    public class AuthService : IAuthService
    {
        public string GetToken(AuthModel authModel)
        {
            var client = new RestClient("https://api.marvelous.com");
            var request = new RestRequest("/login/", Method.Post);
            request.AddJsonBody(authModel);
            var response = client.ExecuteAsync(request);
            return response.ToString();
        }
    }
}
