using MarvelousService.DataLayer.Entities;

namespace MarvelousService.DataLayer.Interfaces
{
    public interface IServiceToLeadRepository
    {
        public ServiceToLead GetServiceToLeadById(int id);
        int AddServiceToLead(ServiceToLead service);
        int AddPeriod(ServicePeriod period);
        List<ServiceToLead> GetByLeadId(int id);
    }
}
