using MarvelousService.BusinessLayer.Models;
using MarvelousService.DataLayer.Entities;
using System.Collections.Generic;

namespace MarvelousService.BusinessLayer.Tests.TestCaseSource
{
    public class ResourceServiceTestCaseSource
    {
        public ResourceModel AddResourceModelTest()
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


        public Resource AddResourceTest()
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

        public List<Resource> AddAllServiceTest()
        {
            List<Resource> resource = new List<Resource>
            {
                new Resource()
                {
                    Id = 1,
                    Name = "qwe",
                    Description = "QWEQWE",
                    Price = 1500,
                    IsDeleted = false,
                },

                new Resource()
                {
                    Id = 2,
                    Name = "ewq",
                    Description = "ewqewq",
                    Price = 2000,
                    IsDeleted = true,
                },

                new Resource()
                {
                    Id = 3,
                    Name = "tmp",
                    Description = "tmptmp",
                    Price = 3000,
                    IsDeleted = false,
                },
                new Resource()
                {
                    Id = 4,
                    Name = "qwerty",
                    Description = "qwerty",
                    Price = 8000,
                    IsDeleted = false,
                },

            };

            return resource;
        }

        public List<Resource> GetAllActiveResourseTest()
        {
            List<Resource> resource = new List<Resource>
            {
                new Resource()
                {
                    Id = 1,
                    Name = "qwe",
                    Description = "QWEQWE",
                    Price = 1500,
                    IsDeleted = false,
                },


                new Resource()
                {
                    Id = 3,
                    Name = "tmp",
                    Description = "tmptmp",
                    Price = 3000,
                    IsDeleted = false,
                },
                new Resource()
                {
                    Id = 4,
                    Name = "qwerty",
                    Description = "qwerty",
                    Price = 8000,
                    IsDeleted = false,
                },

            };

            return resource;
        }

    }
}

