using Marvelous.Contracts.Enums;
using MarvelousService.BusinessLayer.Models;

namespace MarvelousService.BusinessLayer.Helpers
{
    public class RegularRoleStrategy : IRoleStrategy
    {
        public int Id { get => (int)Role.Regular; }

        public void GiveDiscountToLead(LeadResourceModel leadResourceModel, Role role)
        {

        }
    }
}
