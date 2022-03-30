using MarvelousService.DataLayer.Enums;

namespace MarvelousService.BusinessLayer.Models
{
    public class ServiceToLeadModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public Period Period { get; set; }
        public Status Status { get; set; }
        public int LeadId { get; set; }
        public ServiceModel ServiceId { get; set; }
        public List<ServicePaymentModel> servicePayments { get; set; }

        public decimal GetTotalPrice(decimal price, Period p)
        {
            // приведение enum к конкретному классу
            if (p == Period.Year)
            {
                SubscriptionTime time = new Year();
                time.GetPrice(price);
            }

            return price;
        }
    }
}
