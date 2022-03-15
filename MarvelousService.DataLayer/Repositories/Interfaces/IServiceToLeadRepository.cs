using MarvelousService.DataLayer.Entities;

namespace MarvelousService.DataLayer.Interfaces
{
    public interface IServiceToLeadRepository
    {
        public ServiceToLead GetServiceToLeadById(int id);
        int AddServiceToLead(ServiceToLead service);        
        List<ServiceToLead> GetByLeadId(int id);
    }
}
