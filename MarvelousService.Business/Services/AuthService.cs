using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Services.Interfaces;
using RestSharp;
using System.Net;

namespace MarvelousService.BusinessLayer.Services
{
    public class AuthService : IAuthService
    {
        private const string _url = "https://piter-education.ru:6010";

        public Task<RestResponse> GetToken(AuthModel authModel)
        {
            var client = new RestClient(_url);
            var request = new RestRequest("/login/", Method.Post);
            request.AddJsonBody(authModel);
            var response = client.ExecuteAsync(request);

            if (response.Status is (TaskStatus)HttpStatusCode.OK)
            {
                return response;
            }
            else throw new Exception();
        }
    }
}
