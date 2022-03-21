using MarvelousService.BusinessLayer.Models;

namespace MarvelousService.BusinessLayer
{
    public class YearPeriod : OneTimePeriod
    {
        private const int _yearCoef = 50;

        public double GetPrice(double price)
        {
            return base.GetPrice(price) * _yearCoef;
        }
    }
}
