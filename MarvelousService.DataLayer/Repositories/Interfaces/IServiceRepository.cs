using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelousService.DataLayer.Interfaces
{
    public interface IServiceRepository
    {
        public Service GetServiceById(int id);
        int AddService(Service service);        
        List<Service> GetByLeadId(int id);
        
    }
}
