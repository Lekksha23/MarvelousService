using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelousService.DataLayer.Entities
{
    public class ServiceToLead
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public int Period { get; set; }
        public int Price { get; set; }
        public int Status{ get; set; }
        public int LeadId{ get; set; }
        public int ServiceId { get; set; }
        public int TransactionId { get; set; }
    }
}
