namespace MarvelousService.BusinessLayer.Models
{
    public class ServicePaymentModel
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public int ServiceToLeadId { get; set; }
    }
}
