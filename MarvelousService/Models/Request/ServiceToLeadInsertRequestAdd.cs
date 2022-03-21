using MarvelousService.DataLayer.Enums;

namespace MarvelousService.API.Models.Request
{
    public class ServiceToLeadInsertRequestAdd
    {
        public ServiceType Type { get; set; }
        public decimal Price { get; set; }
        public Period Period { get; set; }
        public Status Status { get; set; }
        public int ServiceId { get; set; }
    }
}
