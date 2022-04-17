using MarvelousService.BusinessLayer.Models;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Enums;
using System.Collections.Generic;

namespace MarvelousService.BusinessLayer.Tests.TestCaseSource
{
    public static class LeadResourceTestData
    {
        public static LeadResourceModel GetLeadResourceModelWithWeekPeriodForTests()
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

        public static LeadResource GetLeadResourceWithWeekPeriodForTests()
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

        public static LeadResourceModel GetLeadResourceModelWithOneTimePeriodForTests()
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

        public static LeadResource GetLeadResourceWithOneTimePeriodForTests()
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

        public static LeadResourceModel GetLeadResourceModelWithMonthPeriodForTests()
        {
            var leadResourceModel = new LeadResourceModel
            {
                Id = 1,
                Period = Period.Month,
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

        public static LeadResource GetLeadResourceWithMonthPeriodForTests()
        {
            var leadResource = new LeadResource
            {
                Id = 1,
                Period = Period.Month,
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

        public static LeadResourceModel GetLeadResourceModelWithYearPeriodForTests()
        {
            var leadResourceModel = new LeadResourceModel
            {
                Id = 1,
                Period = Period.Year,
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

        public static LeadResource GetLeadResourceWithYearPeriodForTests()
        {
            var leadResource = new LeadResource
            {
                Id = 1,
                Period = Period.Year,
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

        public static List<LeadResource> GetLeadResourceListForTests()
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
