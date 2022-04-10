using Marvelous.Contracts.Enums;
using MarvelousService.BusinessLayer.Models;

namespace MarvelousService.BusinessLayer.Helpers
{
    public interface IRoleStrategy
    {
        int Id { get; }

        void GiveLeadDiscount(LeadResourceModel leadResourceModel, Role role);
    }
}
