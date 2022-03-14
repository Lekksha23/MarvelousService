namespace MarvelousService.API.Models
{
    public class ServiceInsertRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal OneTimePrice { get; set; }
    }
}
