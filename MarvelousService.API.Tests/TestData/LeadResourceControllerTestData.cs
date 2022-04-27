using Marvelous.Contracts.ResponseModels;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.DataLayer.Enums;
using System.Collections.Generic;

namespace MarvelousService.API.Tests.TestData
{
    public static class LeadResourceControllerTestData
    {
        public static ResourceModel GetResourceModelForTests()
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

        public static LeadResourceModel GetLeadResourceModelForTests()
        {
            return new LeadResourceModel
            {
                Id = 23,
                LeadId = 42,
                Period = Period.Week,
                Price = 7777
            };
        }

        public static List<LeadResourceModel> GetLeadResourceModelListForTests()
        {
            return new List<LeadResourceModel>
            {
                new LeadResourceModel
                {
                    Id = 23,
                    LeadId = 42,
                    Period = Period.Week,
                    Price = 7777
                },
                new LeadResourceModel
                {
                    Id = 24,
                    LeadId = 43,
                    Period = Period.Week,
                    Price = 7777
                }
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
