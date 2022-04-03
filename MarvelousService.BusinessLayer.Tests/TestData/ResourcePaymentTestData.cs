using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Enums;
using System.Collections.Generic;

namespace MarvelousService.BusinessLayer.Tests.TestData
{
    public class ResourcePaymentTestData
    {
        public ResourcePayment GetServicePaymentForTests()
        {
            var servicePayment = new ResourcePayment
            {
                Id = 1, 
                LeadResource = new LeadResource 
                {
                    Id = 2,
                    LeadId = 42,
                    Period = Period.Week,
                    Price = 6000,
                    Resource =  new Resource 
                    { 
                        Id = 2,
                        Name = "Тренинг"
                    },
                    Status = Status.Active},
                TransactionId = 100000
            };
            return servicePayment;
        }

        public List<ResourcePayment> GetListOfServicePaymentsForTests()
        {
            return new List<ResourcePayment>
            {
                new ResourcePayment 
                { 
                    Id = 1,
                    LeadResource = new LeadResource
                    {
                        Id = 2,
                        LeadId= 42,
                        Period = Period.Week,
                        Price = 3000,
                        Resource = new Resource
                        {
                            Id = 3,
                            Name = "Тренинг", 
                            Price = 1000, 
                            Description = "Скучный тренинг", 
                            IsDeleted = false
                        },
                        Status = Status.Active, 
                        ResourcePayments = new List<ResourcePayment>()
                    }
                },
                new ResourcePayment
                {
                    Id = 2,
                    LeadResource = new LeadResource
                    {
                        Id = 2,
                        LeadId= 42,
                        Period = Period.Week,
                        Price = 3000,
                        Resource = new Resource
                        {
                            Id = 3,
                            Name = "Тренинг",
                            Price = 1000,
                            Description = "Скучный тренинг",
                            IsDeleted = false
                        },
                        Status = Status.Active,
                        ResourcePayments = new List<ResourcePayment>()
                    }
                }
            };
        }

    }
}
