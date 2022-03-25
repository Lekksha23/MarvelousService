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
    public class CRMController : Controller
    {
        private readonly IMapper _autoMapper;
        private readonly ICRMService _crmService;
        private readonly ILogger<CRMController> _logger;

        public CRMController(IMapper autoMapper, ICRMService crmService, ILogger<CRMController> logger)
        {
            _logger = logger;
            _autoMapper = autoMapper;
            _crmService = crmService;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [SwaggerOperation("Authentication")]
        public async Task<ActionResult> Login([FromBody] AuthRequest auth)
        {
            _logger.LogInformation($"Poluchen zapros na authentikaciu leada c email = {auth.Email}.");
            var authModel = _autoMapper.Map<AuthModel>(auth);
            var token = await _crmService.GetToken(authModel);
            _logger.LogInformation($"Authentikacia leada c email = {auth.Email} proshhla uspeshno.");
            return Json(token);
        }

    }
}
