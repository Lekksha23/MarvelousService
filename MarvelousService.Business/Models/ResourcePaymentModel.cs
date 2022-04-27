namespace MarvelousService.BusinessLayer.Models
{
    public class ResourcePaymentModel
    {
        public int Id { get; set; }
        public long TransactionId { get; set; }
        public LeadResourceModel LeadResource { get; set; }
    }
}
