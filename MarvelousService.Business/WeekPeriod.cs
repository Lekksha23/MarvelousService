using MarvelousService.BusinessLayer.Models;

namespace MarvelousService.BusinessLayer
{
    public class WeekPeriod : OneTimePeriod
    {
        private const double _weekCoef = 2.5;

        public double GetPrice(double price)
        {
            return base.GetPrice(price) * _weekCoef;
        }
    }
}
