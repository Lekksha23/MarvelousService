namespace MarvelousService.API.Models.Request
{
    public class ServiceUpdateRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal OneTimePrice { get; set; }
    }
}
