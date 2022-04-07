﻿using Marvelous.Contracts.Enums;
using MarvelousService.DataLayer.Enums;

namespace MarvelousService.BusinessLayer.Models
{
    public class LeadResourceModel
    {
        public int Id { get; set; }
        public Period Period { get; set; }
        public Role LeadRole { get; set; }    
        public ResourceModel Resource { get; set; }
        public Status Status { get; set; }
        public int LeadId { get; set; }
        public List<ResourcePaymentModel> ResourcePayments { get; set; }

        public decimal Price
        {
            get
            {
                SubscriptionTime time = new OneTime();

                time = Period switch
                {
                    Period.Week => new Week(),
                    Period.Month => new Month(),
                    Period.Year => new Year(),
                    _ => time
                };
                return time.GetPrice(Resource.Price);
            }
            set => value;
        }
    }
}
