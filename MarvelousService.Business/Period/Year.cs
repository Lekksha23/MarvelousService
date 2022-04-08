namespace MarvelousService.BusinessLayer
{
    public class Year : SubscriptionTime
    {
        private const int _yearCoef = 20;

        public override decimal GetPrice(decimal resourcePrice)
        {
            return resourcePrice * _yearCoef;
        }
    }
}
