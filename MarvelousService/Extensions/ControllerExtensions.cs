using Marvelous.Contracts.Enums;
using MarvelousService.API.Models;
using MarvelousService.BusinessLayer.Exceptions;
using MarvelousService.BusinessLayer.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MarvelousService.API.Extensions
{
    public  class ControllerExtensions : Controller
    {
        private readonly IReqvestHelper _reqvestHelper;
        private readonly ILogger  _logger;


        public ControllerExtensions(IReqvestHelper reqvestHelper, ILogger logger)
        {
            _reqvestHelper = reqvestHelper;
            _logger = logger;
        }



        public  async Task CheckRole(params Role[] roles)
        {
            //вызов моей проверки и получение LeadIdentity
            var lead = await _reqvestHelper.SendRequestCheckValidateToken(HttpContext.Request.Headers.Authorization[0]);
            if (!roles.Select(r => r.ToString()).Contains(lead.Data.Role))
            {

                throw new RoleException("");
            }
        }
    }
}