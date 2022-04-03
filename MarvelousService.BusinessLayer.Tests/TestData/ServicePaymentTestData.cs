using MarvelousService.BusinessLayer.Models;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Enums;
using System.Collections.Generic;

namespace MarvelousService.BusinessLayer.Tests.TestData
{
    public class ServicePaymentTestData
    {
        public ResourcePayment GetServicePaymentForTests()
        {
            var servicePayment = new ResourcePayment
            {
                Id = 1, 
                ServiceToLeadId = new LeadResource 
                {
                    Id = 2,
                    LeadId = 42,
                    Period = Period.Week,
                    Price = 6000,
                    ServiceId =  new Resource 
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
                    ServiceToLeadId = new LeadResource
                    {
                        Id = 2,
                        LeadId= 42,
                        Period = Period.Week,
                        Price = 3000,
                        ServiceId = new Resource
                        {
                            Id = 3,
                            Name = "Тренинг", 
                            Price = 1000, 
                            Description = "Скучный тренинг", 
                            IsDeleted = false
                        },
                        Status = Status.Active, 
                        servicePayments = new List<ResourcePayment>()
                    }
                },
                new ResourcePayment
                {
                    Id = 2,
                    ServiceToLeadId = new LeadResource
                    {
                        Id = 2,
                        LeadId= 42,
                        Period = Period.Week,
                        Price = 3000,
                        ServiceId = new Resource
                        {
                            Id = 3,
                            Name = "Тренинг",
                            Price = 1000,
                            Description = "Скучный тренинг",
                            IsDeleted = false
                        },
                        Status = Status.Active,
                        servicePayments = new List<ResourcePayment>()
                    }
                }
            };
        }
    }
}
