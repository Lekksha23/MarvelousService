using MarvelousService.DataLayer.Enums;

namespace MarvelousService.BusinessLayer.Models
{
    public class ServiceModel
    {
        public int Id { get; set; }
        public ServiceName Name { get; set; }
        public ServiceType Type { get; set; }
        public Period Period { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Status Status { get; set; }
        public int LeadId { get; set; }
        public int TransactionId { get; set; }
    }
}
