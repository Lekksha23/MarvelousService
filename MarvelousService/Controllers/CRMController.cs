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
            _autoMapper = autoMapper;
            _crmService = crmService;
            _logger = logger;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [SwaggerOperation("Authentication")]
        public async Task<ActionResult> Login([FromBody] AuthRequest auth)
        {
            _logger.LogInformation($"Получен запрос на аутентикацию лида c email = {auth.Email}.");

            var authModel = _autoMapper.Map<AuthModel>(auth);

            var token = await _crmService.GetToken(authModel);

            _logger.LogInformation($"Аутентикация лида c email = {auth.Email}, прошла успешна.");

            return Json(token);
        }

        [HttpPost("registrate")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [SwaggerOperation("Registrate new lead")]
        public async Task<ActionResult> Registrate([FromBody] LeadInsertRequest lead)
        {
            _logger.LogInformation($"Получен запрос на регистрацию лида c email = {lead.Email}.");

            var leadModel = _autoMapper.Map<LeadModel>(lead);

            var id = await _crmService.RegistrateLead(leadModel);

            _logger.LogInformation($"Регистрация лида c email = {lead.Email}, прошла успешна.");

            return StatusCode(StatusCodes.Status201Created, id);
        }
    }
}
