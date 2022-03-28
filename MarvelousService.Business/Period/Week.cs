namespace MarvelousService.BusinessLayer
{
    public class Week : SubscriptionTime
    {
        private const int _weekCoef = 2;

        public override decimal GetPrice(decimal price)
        {
            return price * _weekCoef;
        }
    }
}
