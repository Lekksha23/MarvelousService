using Marvelous.Contracts.Enums;
using MarvelousService.BusinessLayer.Exceptions;
using MarvelousService.BusinessLayer.Models;
using NLog;

namespace MarvelousService.BusinessLayer.Helpers
{
    public class AdminRoleStrategy : IRoleStrategy
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public int Id { get => (int)Role.Admin; }

        public void GiveLeadDiscount(LeadResourceModel leadResourceModel, Role role)
        {
            throw new RoleException("User with role Admin can't buy any resources.");
            _logger.Error("User with role Admin was trying to buy a resource.");
        }
    }
}
