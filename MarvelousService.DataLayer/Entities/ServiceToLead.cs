using MarvelousService.DataLayer.Enums;

namespace MarvelousService.DataLayer.Entities
{
    public class ServiceToLead
    {
        public int Id { get; set; }
        public Period Period { get; set; }
        public decimal Price { get; set; }
        public Status Status{ get; set; }
        public int LeadId{ get; set; }
        public int ServiceId { get; set; }
        public List<ServicePayment> servicePayments { get; set; }   
    }
}
