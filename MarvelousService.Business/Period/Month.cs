namespace MarvelousService.BusinessLayer
{
    public class Month : SubscriptionTime
    {
        private const int _monthCoef = 4;

        public override decimal GetPrice(decimal price)
        {
            return price * _monthCoef;
        }
    }
}
