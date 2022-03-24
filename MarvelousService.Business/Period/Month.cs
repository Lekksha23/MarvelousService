namespace MarvelousService.BusinessLayer
{
    public class Month : ServiceToLeadBase, IServiceToLead
    {
        private const int _monthCoef = 5;

        public decimal GetPrice(decimal price)
        {
            return price * _monthCoef;
        }
    }
}
