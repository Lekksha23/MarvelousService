using Marvelous.Contracts.Enums;
using MarvelousService.BusinessLayer.Models;

namespace MarvelousService.BusinessLayer.Helpers
{
    public class VipRoleStrategy : IRoleStrategy
    {
        private const double _discountVIP = 0.9;
        public int Id { get => (int)Role.Vip; }

        public void GiveDiscountToLead(LeadResourceModel leadResourceModel, Role role)
        {
            leadResourceModel.Price *= (decimal)_discountVIP;
        }
    }
}
