using AutoMapper;
using MarvelousService.API.Models;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Services;
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
        private readonly ICRMClient _crmClient;
        private readonly ILogger<CRMController> _logger;

        public CRMController(IMapper autoMapper, ICRMClient crmService, ILogger<CRMController> logger)
        {
            _logger = logger;
            _autoMapper = autoMapper;
            _crmClient = crmService;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [SwaggerOperation("Authentication")]
        public async Task<ActionResult> Login([FromBody] AuthRequest auth)
        {
            _logger.LogInformation($"Query for authentication user with email:{auth.Email}.");
            var authModel = _autoMapper.Map<AuthModel>(auth);
            var token = await _crmClient.GetToken(authModel);
            _logger.LogInformation($"Authentication of user with email:{auth.Email} successfully completed.");
            return Json(token);
        }

        [HttpPost("authorize")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation("Authorization")]
        public async Task<ActionResult> Authorize([FromBody] AuthRequest auth)
        {
            _logger.LogInformation($"Query for authorization user with email:{auth.Email}.");
            var authModel = _autoMapper.Map<AuthModel>(auth);
            await _crmClient.Authorize(authModel);
            _logger.LogInformation($"Authorization of user with email:{auth.Email} successfully completed.");
            return Ok();
        }

        [HttpPost("registrate")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [SwaggerOperation("Registrate a new lead")]
        public async Task<ActionResult<int>> RegistrateLead([FromBody] LeadInsertRequest leadInsertRequest)
        {
            _logger.LogInformation($"Query for registration new lead with name:{leadInsertRequest.Name} and email: {leadInsertRequest.Email}");
            var leadModel = _autoMapper.Map<LeadModel>(leadInsertRequest);
            var leadId = await _crmClient.AddLead(leadModel);
            _logger.LogInformation($"A New lead with email:{leadInsertRequest.Email} was successfully added.");
            return Ok(leadId);
        }

    }
}
