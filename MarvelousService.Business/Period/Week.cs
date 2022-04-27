namespace MarvelousService.BusinessLayer
{
    public class Week : SubscriptionTime
    {
        private const int _weekCoef = 2;

        public override decimal CountPrice(decimal resourcePrice)
        {
            return resourcePrice * _weekCoef;
        }
    }
}
