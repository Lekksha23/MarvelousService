using Marvelous.Contracts.Enums;
using MarvelousService.BusinessLayer.Models;

namespace MarvelousService.BusinessLayer.Helpers
{
    public class VIPRoleStrategy : IRoleStrategy
    {
        private const double _discountVIP = 0.9;
        public int Id { get => (int)Role.Vip; }

        public void GiveLeadDiscount(LeadResourceModel leadResourceModel, Role role)
        {
            leadResourceModel.Price *= (decimal)_discountVIP;
        }
    }
}
