using AutoMapper;
using MarvelousService.API.Models;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MarvelousService.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IMapper _autoMapper;
        private readonly IAuthService _authService;

        public AuthController(IMapper autoMapper, IAuthService authService)
        {
            _autoMapper = autoMapper;
            _authService = authService;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [SwaggerOperation("Authentication")]
        public ActionResult Login([FromBody] AuthRequest auth)
        {
            var authModel = _autoMapper.Map<AuthModel>(auth);
            var token = _authService.GetToken(authModel);
            return Json(token);
        }
    }
}
