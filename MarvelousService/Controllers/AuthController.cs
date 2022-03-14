using CRM.APILayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace CRM.APILayer.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [AllowAnonymous]
    public class AuthController : Controller
    {
        [HttpPost("login")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [Description("Authentication")]
        public ActionResult Login([FromBody] AuthRequest auth)
        {
            var token = _authService.GetToken(authModel);
            return Json(token);
        }
    }
}
