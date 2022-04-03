using MarvelousService.BusinessLayer.Models;
using MarvelousService.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelousService.BusinessLayer.Tests.TestCaseSource
{
    public class ServiceToServiceTestCaseSource
    {
        public ResourceModel AddServiceModelTest()
        {
            ResourceModel serviceModel = new ResourceModel
            {
                Id = 1,
                Name = "qwe",
                Description = "QWEQWE",
                Price = 1500,
                IsDeleted = false,
            };

            return serviceModel;
        }
        public Resource AddServiceTest()
        {
            Resource service = new Resource
            {
                Id = 1,
                Name = "qwe",
                Description = "QWEQWE",
                Price = 1500,
                IsDeleted = false,
            };

            return service;
        }

    }
           
        
}

