using MarvelousService.BusinessLayer.Models;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Enums;
using System.Collections.Generic;

namespace MarvelousService.BusinessLayer.Tests.TestData
{
    public class ServicePaymentTestData
    {
        public ServicePayment GetServicePaymentForTests()
        {
            var servicePayment = new ServicePayment
            {
                Id = 1, 
                ServiceToLeadId = new ServiceToLead 
                {
                    Id = 2,
                    LeadId = 42,
                    Period = Period.Week,
                    Price = 6000,
                    ServiceId =  new Service 
                    { 
                        Id = 2,
                        Name = "Тренинг"
                    },
                    Status = Status.Active},
                TransactionId = 100000
            };
            return servicePayment;
        }

        public List<ServicePayment> GetListOfServicePaymentsForTests()
        {
            return new List<ServicePayment>
            {
                new ServicePayment 
                { 
                    Id = 1,
                    ServiceToLeadId = new ServiceToLead
                    {
                        Id = 2,
                        LeadId= 42,
                        Period = Period.Week,
                        Price = 3000,
                        ServiceId = new Service
                        {
                            Id = 3,
                            Name = "Тренинг", 
                            Price = 1000, 
                            Description = "Скучный тренинг", 
                            IsDeleted = false
                        },
                        Status = Status.Active, 
                        servicePayments = new List<ServicePayment>()
                    }
                },
                new ServicePayment
                {
                    Id = 2,
                    ServiceToLeadId = new ServiceToLead
                    {
                        Id = 2,
                        LeadId= 42,
                        Period = Period.Week,
                        Price = 3000,
                        ServiceId = new Service
                        {
                            Id = 3,
                            Name = "Тренинг",
                            Price = 1000,
                            Description = "Скучный тренинг",
                            IsDeleted = false
                        },
                        Status = Status.Active,
                        servicePayments = new List<ServicePayment>()
                    }
                }
            };
        }
    }
}
