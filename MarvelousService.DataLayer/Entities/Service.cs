using MarvelousService.DataLayer.Enums;

namespace MarvelousService.DataLayer.Entities
{
    public class Service
    {
        public int Id { get; set; }
        public ServiceName Name { get; set; }
        public string Description { get; set; }
        public decimal OneTimePrice { get; set; }
        public bool IsDeleted { get; set; }
    }
}
