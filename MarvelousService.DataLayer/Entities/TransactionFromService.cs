using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelousService.DataLayer.Entities
{
    public class TransactionFromService
    {
        public int Id { get; set; }
        public decimal OneTimePrice { get; set; }
        public int TransactionId { get; set; }
    }
}
