﻿using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Enums;

namespace MarvelousService.BusinessLayer.Models
{
    public class LeadResourceModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public Period Period { get; set; }
        public Status Status { get; set; }
        public int LeadId { get; set; }
        public ResourceModel Resource { get; set; }
        public List<ResourcePaymentModel> ResourcePayments { get; set; }

        public decimal GetTotalPrice(decimal price, Period period)
        {
            if (period == Period.Year)
            {
                SubscriptionTime time = new Year();
                return time.GetPrice(price);
            }
            else if (period == Period.Month)
            {
                SubscriptionTime time = new Month();
                return time.GetPrice(price);
            }
            else if (period == Period.Week)
            {
                SubscriptionTime time = new Week();
                return time.GetPrice(price);
            }
            else
            {
                return price;
            }
        }
    }
}
