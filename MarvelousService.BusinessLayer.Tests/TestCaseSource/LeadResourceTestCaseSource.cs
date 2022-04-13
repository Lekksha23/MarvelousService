using MarvelousService.BusinessLayer.Models;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Enums;
using System.Collections.Generic;

namespace MarvelousService.BusinessLayer.Tests.TestCaseSource
{
    public class LeadResourceTestCaseSource
    {
        public LeadResourceModel GetLeadResourceModelWithWeekPeriodForTests()
        {
            LeadResourceModel leadResourceModel = new LeadResourceModel
            {
                Id = 1,
                Period = Period.Week,
                LeadId = 1,
                Resource = new ResourceModel
                {
                    Id = 1,
                    Name = "qwe",
                    Description = "qweqwe",
                    Price = 1800,
                    IsDeleted = false
                }
            }; 
            return leadResourceModel;
        }

        public LeadResource GetLeadResourceWithWeekPeriodForTests()
        {
            LeadResource leadResource = new LeadResource
            {
                Id = 1,
                Period = Period.Week,
                Price = 3240.0M,
                Status = Status.Active,
                LeadId = 1,
                Resource = new Resource
                {
                    Id = 1,
                    Name = "qwe",
                    Description = "qweqwe",
                    Price = 1800,
                    Type = ServiceType.Subscription,
                    IsDeleted = false
                },
                ResourcePayments = new List<ResourcePayment>()
            };
            return leadResource;
        }
    }
}
