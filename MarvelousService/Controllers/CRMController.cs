using AutoMapper;
using FluentValidation;
using MarvelousService.API.Extensions;
using MarvelousService.API.Models;
using MarvelousService.BusinessLayer.Clients;
using MarvelousService.BusinessLayer.Helpers;
using MarvelousService.BusinessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MarvelousService.API.Controllers
{
    [ApiController]
    [Route("api/crm")]
    [AllowAnonymous]
    [SwaggerTag("This controller is used to registrate a new lead in CRM service.")]
    public class CrmController : ControllerExtensions
    {
        private readonly ICrmClient _crmClient;
        private readonly IMapper _autoMapper;
        private readonly IRequestHelper _requestHelper;
        private readonly ILogger<CrmController> _logger;
        private readonly IValidator<LeadInsertRequest> _leadInsertRequestValidator;

        public CrmController(
            ICrmClient crmClient,
            IMapper autoMapper,
            ILogger<CrmController> logger,
            IRequestHelper requestHelper,
            IValidator<LeadInsertRequest> leadInsertRequestValidator) : base (requestHelper, logger)
        {
            _crmClient = crmClient;
            _autoMapper = autoMapper;
            _logger = logger;
            _requestHelper = requestHelper;
            _leadInsertRequestValidator = leadInsertRequestValidator;
        }

        [HttpPost("registrate")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [SwaggerOperation("Registrate a new lead. Roles: Anonymous")]
        public async Task<ActionResult<int>> RegistrateLead([FromBody] LeadInsertRequest leadInsertRequest)
        {
            var validationResult = await _leadInsertRequestValidator.ValidateAsync(leadInsertRequest);

            if (validationResult.IsValid)
            {
                _logger.LogInformation($"Query for registration new lead with name: {leadInsertRequest.Name} and email: {leadInsertRequest.Email}");
                var leadModel = _autoMapper.Map<LeadModel>(leadInsertRequest);
                var leadId = await _crmClient.AddLead(leadModel);
                _logger.LogInformation($"A new lead with email: {leadInsertRequest.Email} was successfully added.");
                return Ok(leadId);
            }
            else
            {
                _logger.LogError($"Error: {validationResult}");
                throw new ValidationException($"{validationResult}");
            }
        }
    }
}
