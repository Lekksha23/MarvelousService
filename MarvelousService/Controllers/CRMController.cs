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

        public CRMController(IMapper autoMapper, ICRMService crmService)
        {
            _autoMapper = autoMapper;
            _crmService = crmService;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [SwaggerOperation("Authentication")]
        public async Task<ActionResult> Login([FromBody] AuthRequest auth)
        {
            var authModel = _autoMapper.Map<AuthModel>(auth);
            var token = await _crmService.GetToken(authModel);
            return Json(token);
        }

        [HttpPost("registrate")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [SwaggerOperation("Registrate new lead")]
        public async Task<ActionResult> Registrate([FromBody] LeadInsertRequest lead)
        {
            var leadModel = _autoMapper.Map<LeadModel>(lead);
            var id = await _crmService.RegistrateLead(leadModel);
            return StatusCode(StatusCodes.Status201Created, id);
        }
    }
}
