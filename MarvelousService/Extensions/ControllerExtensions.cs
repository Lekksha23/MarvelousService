using FluentValidation;
using Marvelous.Contracts.Enums;
using Marvelous.Contracts.ResponseModels;
using MarvelousService.BusinessLayer.Exceptions;
using MarvelousService.BusinessLayer.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace MarvelousService.API.Extensions
{
    public class ControllerExtensions : Controller
    {
        private readonly IRequestHelper _requestHelper;
        private readonly ILogger _logger;

        public ControllerExtensions(IRequestHelper reqvestHelper, ILogger logger)
        {
            _requestHelper = reqvestHelper;
            _logger = logger;
        }

        protected async Task<IdentityResponseModel> CheckRole(params Role[] roles)
        {
            _logger.LogInformation($"Query for checking role in the IdentityService");
            var lead = await _requestHelper.SendRequestToValidateToken(HttpContext.Request.Headers.Authorization.First());
            var leadRole = lead.Data.Role;
            if (!roles.Select(r => r.ToString()).Contains(leadRole))
            {
                _logger.LogError($"User with role:{leadRole} don't have acces to the method");
                throw new ForbiddenException($"User with role:{leadRole} don't have acces to this method");
            }
            return lead.Data;
        }

        protected void Validate<T>(T requestModel, IValidator<T> validator)
        {
            if (requestModel == null)
            {
                var ex = new BadRequestException("You must specify the details in the request body");
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
            var validationResult = validator.Validate(requestModel);
            if (!validationResult.IsValid)
            {
                var ex = new ValidationException(validationResult.Errors);
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }
    }
}