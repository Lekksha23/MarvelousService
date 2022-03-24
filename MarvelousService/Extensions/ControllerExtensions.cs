using Marvelous.Contracts;
using MarvelousService.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MarvelousService.API.Extensions
{
    public static class ControllerExtensions
    {
        public static LeadIdentity GetLeadFromToken(this Controller controller)
        {
            var identity = controller.HttpContext.User.Identity as ClaimsIdentity;
            var leadIdentity = new LeadIdentity();
            leadIdentity.Id = int.Parse(identity.Claims.ToList()
                .Where(c => c.Type == ClaimTypes.UserData)
                .Select(c => c.Value)
                .SingleOrDefault());
            leadIdentity.Role = Enum.Parse<Role>(identity.Claims.ToList()
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .SingleOrDefault());
            return leadIdentity;
        }
    }
}