using Microsoft.IdentityModel.Tokens;
using RestSharp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CRM.BusinessLayer.Services
{
    public class AuthService : IAuthService
    {
        public string GetToken(AuthModel authModel)
        {
            //Lead entity = _leadRepo.GetByEmail(email);

            //ExceptionsHelper.ThrowIfEmailNotFound(email, entity);
            //ExceptionsHelper.ThrowIfLeadWasBanned(entity.Id, entity);
            //ExceptionsHelper.ThrowIfPasswordIsIncorrected(pass, entity.Password);

            //List<Claim> claims = new List<Claim> {
            //    new Claim(ClaimTypes.Email, entity.Email),
            //    new Claim(ClaimTypes.UserData, entity.Id.ToString()),
            //    new Claim(ClaimTypes.Role, entity.Role.ToString())
            //};

            //var jwt = new JwtSecurityToken(
            //                issuer: AuthOptions.Issuer,
            //                audience: AuthOptions.Audience,
            //                claims: claims,
            //                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(30)),
            //                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var client = new RestClient("https://api.marvelous.com");
            var request = new RestRequest("/login/", Method.Post);
            request.AddJsonBody(authModel);
            var response = client.ExecuteAsync(request);

            return response;

            return new JwtSecurityTokenHandler().WriteToken(jwt);

        }
    }
}
