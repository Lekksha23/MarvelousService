namespace MarvelousService.DataLayer.Entities
{
    public class TransactionFromService
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int TransactionId { get; set; }
        public int ServiceToLeadId { get; set; }
    }
}
