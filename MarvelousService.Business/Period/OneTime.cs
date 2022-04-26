namespace MarvelousService.BusinessLayer.Models
{
    public class OneTime : SubscriptionTime
    {
        public override decimal CountPrice(decimal resourcePrice)
        {
            return resourcePrice;
        }
    }
}
