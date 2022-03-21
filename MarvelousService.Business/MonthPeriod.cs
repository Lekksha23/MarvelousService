using MarvelousService.BusinessLayer.Models;

namespace MarvelousService.BusinessLayer
{
    public class MonthPeriod : OneTimePeriod
    {
        private const int _monthCoef = 5;

        public double GetPrice(double price)
        {
            return base.GetPrice(price) * _monthCoef;
        }
    }
}
