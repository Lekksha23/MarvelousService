using Marvelous.Contracts.Enums;
using MarvelousService.BusinessLayer.Exceptions;
using MarvelousService.BusinessLayer.Models;
using Microsoft.Extensions.Logging;

namespace MarvelousService.BusinessLayer.Helpers
{
    public class AdminRoleStrategy : IRoleStrategy
    {
        private readonly ILogger<AdminRoleStrategy> _logger;
        public int Id { get => (int)Role.Admin; }

        //public AdminRoleStrategy(ILogger<AdminRoleStrategy> logger)
        //{
        //    _logger = logger;
        //}

        public void GiveLeadDiscount(LeadResourceModel leadResourceModel, Role role)
        {
            throw new RoleException("User with role Admin can't buy any resources.");
            //_logger.LogError("User with role Admin was trying to buy a resource.");
        }
    }
}
