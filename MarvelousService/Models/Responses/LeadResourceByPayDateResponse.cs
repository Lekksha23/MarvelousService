namespace MarvelousService.API.Models
{
    public class LeadResourceByPayDateResponse
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int LeadId { get; set; }
        public ResourceForPayDateResponse Resource { get; set; }
    }
}
