namespace MarvelousService.BusinessLayer.Models
{
    public class OneTime : SubscriptionTime
    {
        public override decimal GetPrice(decimal price)
        {
            return price;
        }
    }
}
