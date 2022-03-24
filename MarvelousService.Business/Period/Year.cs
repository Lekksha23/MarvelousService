namespace MarvelousService.BusinessLayer
{
    public class Year : ServiceToLeadBase, IServiceToLead
    {
        private const int _yearCoef = 50;

        public decimal GetPrice(decimal price)
        {
            return price * _yearCoef;
        }
    }
}
