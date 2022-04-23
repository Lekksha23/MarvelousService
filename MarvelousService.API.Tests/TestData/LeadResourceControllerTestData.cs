using Marvelous.Contracts.ResponseModels;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.DataLayer.Enums;

namespace MarvelousService.API.Tests.TestData
{
    public static class LeadResourceControllerTestData
    {
        public static ResourceModel GetResourceForTests()
        {
            return new ResourceModel
            {
                Id = 3,
                Description = "Clever and funny description",
                IsDeleted = false,
                Name = "Интересная подписка",
                Price = 7777,
                Type = ServiceType.Subscription
            };
        }

        public static IdentityResponseModel GetIdentityResponseModel()
        {
            return new IdentityResponseModel
            {
                Id = 10200,
                IssuerMicroservice = "DoctorWho",
                Role = "Regular"
            };
        }
    }
}
