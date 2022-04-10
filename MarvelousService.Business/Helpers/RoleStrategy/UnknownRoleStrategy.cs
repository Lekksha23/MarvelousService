using Marvelous.Contracts.Enums;
using MarvelousService.BusinessLayer.Exceptions;
using MarvelousService.BusinessLayer.Models;
using Microsoft.Extensions.Logging;

namespace MarvelousService.BusinessLayer.Helpers
{
    public class UknownRoleStrategy : IRoleStrategy
    {
        private readonly ILogger<AdminRoleStrategy> _logger;
        public int Id { get => 0 ; }

        //public UknownRoleStrategy(ILogger<AdminRoleStrategy> logger)
        //{
        //    _logger = logger;
        //}

        public void GiveLeadDiscount(LeadResourceModel leadResourceModel, Role role)
        {
            throw new RoleException("Unknown role.");
            //_logger.LogError("User with unknown role was trying to buy a resource.");
        }
    }
}
