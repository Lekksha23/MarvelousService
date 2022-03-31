
using MarvelousService.BusinessLayer.Models;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Enums;
using System.Collections.Generic;

namespace MarvelousService.BusinessLayer.Tests.TestCaseSource
{
    public class ServiceToLeadTestCaseSource
    {
        public ServiceToLeadModel AddServiceToleadModelTest()
        {
            ServiceToLeadModel serviceToLeadModel = new ServiceToLeadModel
            {
                Id = 1,
                Price = 1800,
                Period = (Period)1,
                Status = (Status)1,
                LeadId = 1,
                ServiceId = new ServiceModel
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
        public ServiceToLead AddServiceLeadTest()
        {
            ServiceToLead serviceToLead = new ServiceToLead
            {
                Id = 1,
                Period = (Period)1,
                Price = 2000,
                Status = (Status)1,
                LeadId = 1,
                ServiceId = new Service
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
