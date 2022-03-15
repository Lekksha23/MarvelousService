using MarvelousService.DataLayer.Enums;

namespace MarvelousService.BusinessLayer.Models
{
    public class ServiceToLeadModel
    {
        private const int _weekCoef = 6;
        private const int _monthCoef = 25;
        private const int _yearCoef = 305;

        public int Id { get; set; }
        public ServiceType Type { get; set; }
        public Period Period { get; set; }
        public Status Status { get; set; }
        public int LeadId { get; set; }
        public int ServiceId { get; set; }
        public int TransactionId { get; set; }

        public decimal Price
        {
            get
            {
                if (Period is Period.OneTime)
                {
                    return Price;
                }
                else if (Period is Period.Week)
                {
                    return Price * _weekCoef;
                }
                else if (Period is Period.Month)
                {
                    return Price * _monthCoef;
                }
                else return Price * _yearCoef;
            }
            private set { }
        }
    }
}
