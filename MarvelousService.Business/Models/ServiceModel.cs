using MarvelousService.DataLayer.Entities;

namespace MarvelousService.BusinessLayer.Models
{
    public class ServiceModel 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal OneTimePrice { get; set; }
        public bool IsDeleted { get; set; }

    }
}
