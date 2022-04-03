namespace MarvelousService.DataLayer.Entities
{
    public class ResourcePayment
    {
        public int Id { get; set; }
        public long TransactionId { get; set; }
        public LeadResource LeadResource { get; set; }
    }
}
