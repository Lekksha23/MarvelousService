﻿using RestSharp;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Services.Interfaces;

namespace MarvelousService.BusinessLayer.Services
{
    public class AuthService : IAuthService
    {
        public string GetToken(AuthModel authModel)
        {
            string url = "https://api.marvelous.com";
            var client = new RestClient(url);
            var request = new RestRequest("/login/", Method.Post);
            request.AddJsonBody(authModel);
            var response = client.PostAsync(request);

            return response.ToString();
        }
    }
}
