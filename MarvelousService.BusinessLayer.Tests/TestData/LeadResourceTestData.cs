using MarvelousService.BusinessLayer.Models;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Enums;
using System.Collections.Generic;

namespace MarvelousService.BusinessLayer.Tests.TestCaseSource
{
    public class LeadResourceTestData
    {
        public LeadResourceModel GetLeadResourceModelWithWeekPeriodForTests()
        {
            var leadResourceModel = new LeadResourceModel
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
            var leadResource = new LeadResource
            {
                Id = 1,
                Period = Period.Week,
                Status = Status.Active,
                LeadId = 1,
                Resource = new Resource
                {
                    Id = 1,
                    Name = "qwe",
                    Description = "qweqwe",
                    Price = 1800,
                    IsDeleted = false
                },
                ResourcePayments = new List<ResourcePayment>()
            };
            return leadResource;
        }

        public LeadResourceModel GetLeadResourceModelWithOneTimePeriodForTests()
        {
            var leadResourceModel = new LeadResourceModel
            {
                Id = 1,
                Period = Period.OneTime,
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

        public LeadResource GetLeadResourceWithOneTimePeriodForTests()
        {
            var leadResource = new LeadResource
            {
                Id = 1,
                Period = Period.OneTime,
                Status = Status.Active,
                LeadId = 1,
                Resource = new Resource
                {
                    Id = 1,
                    Name = "qwe",
                    Description = "qweqwe",
                    Price = 1800,
                    IsDeleted = false
                },
                ResourcePayments = new List<ResourcePayment>()
            };
            return leadResource;
        }

        public List<LeadResource> GetLeadResourceListForTests()
        {
            var leadResources = new List<LeadResource>
            {
                new LeadResource
                {
                    Id = 1,
                Period = Period.Week,
                Status = Status.Active,
                LeadId = 1,
                Resource = new Resource
                {
                    Id = 1,
                    Name = "qwe",
                    Description = "qweqwe",
                    Price = 1800,
                    IsDeleted = false
                },
                ResourcePayments = new List<ResourcePayment>()
                },
                new LeadResource
                {
                Id = 2,
                Period = Period.OneTime,
                Status = Status.Active,
                LeadId = 2,
                Resource = new Resource
                {
                    Id = 2,
                    Name = "qwe",
                    Description = "qweqwe",
                    Price = 2500,
                    IsDeleted = false
                },
                ResourcePayments = new List<ResourcePayment>()
                }

            };
            return leadResources;
        }
    }
}
