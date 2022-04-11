using AutoMapper;
using MarvelousService.API.Models;
using MarvelousService.BusinessLayer.Clients;
using MarvelousService.BusinessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MarvelousService.API.Controllers
{
    [ApiController]
    [Route("api/crm")]
    [AllowAnonymous]
    public class CRMController : Controller
    {
        private readonly ICRMClient _crmClient;
        private readonly IMapper _autoMapper;
        private readonly ILogger<AuthController> _logger;

        public CRMController(ICRMClient crmClient, IMapper autoMapper, ILogger<AuthController> logger)
        {
            _crmClient = crmClient;
            _autoMapper = autoMapper;
            _logger = logger;
        }

        [HttpPost("registrate")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [SwaggerOperation("Registrate a new lead. Roles: Anonymous")]
        public async Task<ActionResult<int>> RegistrateLead([FromBody] LeadInsertRequest leadInsertRequest)
        {
            _logger.LogInformation($"Query for registration new lead with name:{leadInsertRequest.Name} and email: {leadInsertRequest.Email}");
            var leadModel = _autoMapper.Map<LeadModel>(leadInsertRequest);
            var leadId = await _crmClient.AddLead(leadModel);
            _logger.LogInformation($"A new lead with email:{leadInsertRequest.Email} was successfully added.");
            return Ok(leadId);
        }
    }
}
