using MarvelousService.BusinessLayer.Models;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Enums;

namespace MarvelousService.BusinessLayer.Tests.TestCaseSource
{
    public class LeadResourceTestCaseSource
    {
        public LeadResourceModel AddServiceToleadModelTest()
        {
            LeadResourceModel leadResourceModel = new LeadResourceModel
            {
                Id = 1,
                Price = 1800,
                Period = (Period)1,
                Status = (Status)1,
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

        public LeadResource AddServiceLeadTest()
        {
            LeadResource leadResource = new LeadResource
            {
                Id = 1,
                Period = (Period)1,
                Price = 2000,
                Status = (Status)1,
                LeadId = 1,
                Resource = new Resource
                {
                    Id = 1,
                    Name = "qwe",
                    Description = "qweqwe",
                    Price = 1800,
                    IsDeleted = false
                }
            };
            return leadResource;
        }

    }
}
