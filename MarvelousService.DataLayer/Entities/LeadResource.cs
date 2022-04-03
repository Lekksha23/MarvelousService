using MarvelousService.DataLayer.Enums;

namespace MarvelousService.DataLayer.Entities
{
    public class LeadResource
    {
        public int Id { get; set; }
        public Period Period { get; set; }
        public decimal Price { get; set; }
        public Status Status{ get; set; }
        public int LeadId{ get; set; }
        public Resource Resource { get; set; }
        public List<ResourcePayment> ResourcePayments { get; set; }   
    }
}
