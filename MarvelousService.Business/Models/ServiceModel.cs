namespace MarvelousService.BusinessLayer.Models
{
    public class ServiceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
    }
}
