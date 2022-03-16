﻿using MarvelousService.DataLayer.Entities;

namespace MarvelousService.DataLayer.Repositories.Interfaces
{
    public interface IServiceRepository
    {
        int AddService(Service service);
        void SoftDeleted(int id,Service service);
        int AddPeriod(ServicePeriod period);
        void UpdateService(Service oldService, Service service);
        Service GetServiceById(int id);
    }
}
