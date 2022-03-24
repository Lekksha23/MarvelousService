using MarvelousService.BusinessLayer.Models;
using MarvelousService.DataLayer.Enums;

namespace MarvelousService.BusinessLayer
{
    public abstract class ServiceToLeadBase
    {
        public int Id { get; set; }
        public ServiceType Type { get; set; }
        public decimal Price { get; set; }
        public Period Period { get; set; }
        public Status Status { get; set; }
        public int LeadId { get; set; }
        public int ServiceId { get; set; }
        public List<ServicePaymentModel> servicePayments { get; set; }
    }
}
