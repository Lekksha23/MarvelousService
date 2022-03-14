namespace MarvelousService.API.Models.Request
{
    public class ServiceToLeadInsertRequest
    {
        public int ServiceId { get; set; }
        public int Type { get; set; }
        public int Period { get; set; }
        public int Status { get; set; }
        public int LeadId { get; set; }
        public int TransactionId { get; set; }
    }
}
