namespace MarvelousService.DataLayer.Entities
{
    public class ServicePayment
    {
        public int Id { get; set; }
        public long TransactionId { get; set; }
        public ServiceToLead ServiceToLeadId { get; set; }
    }
}
