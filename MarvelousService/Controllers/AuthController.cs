using Marvelous.Contracts.Enums;
using Marvelous.Contracts.RequestModels;
using MarvelousService.API.Extensions;
using MarvelousService.BusinessLayer.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MarvelousService.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [AllowAnonymous]
    [SwaggerTag("This controller is used to login.")]
    public class AuthController : ControllerExtensions
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IRequestHelper _requestHelper;

        public AuthController(ILogger<AuthController> logger, IRequestHelper requestHelper): base(requestHelper, logger)
        {
            _logger = logger;
            _requestHelper = requestHelper;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [SwaggerOperation("Authentication. Roles: Anonymous")]
        public async Task<ActionResult> Login([FromBody] AuthRequestModel auth)
        {
            _logger.LogInformation($"Query for authentication user with email:{auth.Email}.");
            var token = await _requestHelper.GetTokenForFront<string>(Microservice.MarvelousAuth,auth);
            _logger.LogInformation($"Authentication for user with email:{auth.Email} successfully completed.");
            return Json(token.Content);
        }
    }
}
