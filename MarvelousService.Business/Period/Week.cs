namespace MarvelousService.BusinessLayer
{
    public class Week : ServiceToLeadBase, IServiceToLead
    {
        private const double _weekCoef = 2.5;

        public decimal GetPrice(decimal price)
        {
            return price * (decimal)_weekCoef;
        }
    }
}
