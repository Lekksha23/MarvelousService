using MarvelousService.BusinessLayer.Models;
using MarvelousService.DataLayer.Entities;

namespace MarvelousService.BusinessLayer.Tests.TestCaseSource
{
    public class ResourceServiceTestCaseSource
    {
        public ResourceModel AddServiceModelTest()
        {
            ResourceModel resourceModel = new ResourceModel
            {
                Id = 1,
                Name = "qwe",
                Description = "QWEQWE",
                Price = 1500,
                IsDeleted = false,
            };
            return resourceModel;
        }

        public Resource AddServiceTest()
        {
            Resource resource = new Resource
            {
                Id = 1,
                Name = "qwe",
                Description = "QWEQWE",
                Price = 1500,
                IsDeleted = false,
            };
            return resource;
        }

    }
}

