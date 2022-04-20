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

        protected void CheckRole(IdentityResponseModel identity, params Role[] roles)
        {
            if (identity.Role == null)
            {
                var ex = new ForbiddenException($"Invalid token");
                _logger.LogError(ex.Message);
                throw ex;
            }
            if (!roles.Select(r => r.ToString()).Contains(identity.Role))
            {
                var ex = new ForbiddenException($"Lead {identity.Id} doesn't have access to this endpiont");
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        protected IdentityResponseModel GetIdentity()
        {
            var token = HttpContext.Request.Headers.Authorization.FirstOrDefault();
            if (token == null)
            {
                var ex = new ForbiddenException($"Anonymous doesn't have access to this endpiont");
                _logger.LogError(ex.Message);
                throw ex;
            }
            var identity = _requestHelper.GetLeadIdentityByToken(token).Result;
            return identity;
        }

        protected void Validate<T>(T requestModel, IValidator<T> validator)
        {
            if (requestModel == null)
            {
                var ex = new BadRequestException("You must specify details in the request body");
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