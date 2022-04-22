namespace MarvelousService.API.Models
{
    public class LeadResourceResponse
    {
        public int Id { get; set; }
        public int Period { get; set; }
        public decimal Price { get; set; }
        public ResourceResponse Resource { get; set; }
        public DateTime StartDate { get; set; } 
        public int Status { get; set; }
    }
}
