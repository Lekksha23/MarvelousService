using MarvelousService.DataLayer.Enums;

namespace MarvelousService.BusinessLayer.Models
{
    public class ServiceToLeadModel
    {
        public int Id { get; set; }
        public ServiceType Type { get; set; }
        public decimal Price { get; set; }
        public Period Period { get; set; }
        public Status Status { get; set; }
        public int LeadId { get; set; }
        public int ServiceId { get; set; }
    }
}
