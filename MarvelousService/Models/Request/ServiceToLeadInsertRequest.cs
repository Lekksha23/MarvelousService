namespace MarvelousService.API.Models
{
    public class ServiceToLeadInsertRequest
    {
        public int ServiceId { get; set; }
        public int Type { get; set; }
        public int? Period { get; set; }

    }
}
