using MarvelousService.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelousService.DataLayer.Repositories.Interfaces
{
    public interface IServiceRepository
    {
        int AddService(Service service);
        Service SoftDeleted(Service service);
        Service UpdateService(Service service);
        Service GetServiceById(int id);
    }
}
