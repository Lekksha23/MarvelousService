
using MarvelousService.BusinessLayer.Models;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Enums;
using System.Collections.Generic;

namespace MarvelousService.BusinessLayer.Tests.TestCaseSource
{
    public class ServiceToLeadTestCaseSource
    {
        public LeadResourceModel AddServiceToleadModelTest()
        {
            LeadResourceModel serviceToLeadModel = new LeadResourceModel
            {
                Id = 1,
                Price = 1800,
                Period = (Period)1,
                Status = (Status)1,
                LeadId = 1,
                ServiceId = new ResourceModel
                {
                    Id = 1,
                    Name = "qwe",
                    Description = "qweqwe",
                    Price = 1800,
                    IsDeleted = false
                }

            };
                
            return serviceToLeadModel;
        }
        public LeadResource AddServiceLeadTest()
        {
            LeadResource serviceToLead = new LeadResource
            {
                Id = 1,
                Period = (Period)1,
                Price = 2000,
                Status = (Status)1,
                LeadId = 1,
                ServiceId = new Resource
                {
                    Id = 1,
                    Name = "qwe",
                    Description = "qweqwe",
                    Price = 1800,
                    IsDeleted = false
                }


            };

            return serviceToLead;
        }
    }
}
