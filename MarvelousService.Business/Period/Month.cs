namespace MarvelousService.BusinessLayer
{
    public class Month : SubscriptionTime
    {
        private const int _monthCoef = 4;

        public override decimal CountPrice(decimal resourcePrice)
        {
            return resourcePrice * _monthCoef;
        }
    }
}
