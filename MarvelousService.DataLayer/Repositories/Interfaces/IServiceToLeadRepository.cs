using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelousService.DataLayer.Interfaces
{
    public interface IServiceToLeadRepository
    {
        public ServiceToLead GetServiceToLeadById(int id);
        int AddServiceToLead(ServiceToLead service);
        int AddPeriod(Period period);
        List<ServiceToLead> GetByLeadId(int id);
        
    }
}
