namespace MarvelousService.BusinessLayer.Models
{
    public class OneTime : SubscriptionTime
    {
        public override decimal GetPrice(decimal resourcePrice)
        {
            return resourcePrice;
        }
    }
}
