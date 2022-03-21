namespace MarvelousService.DataLayer.Entities
{
    public class ServicePayment
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public ServiceToLead ServiceToLeadId { get; set; }
    }
}
