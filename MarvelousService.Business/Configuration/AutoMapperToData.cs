using AutoMapper;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.DataLayer.Entities;

namespace CRM.BusinessLayer.Configurations
{
    public class AutoMapperToData : Profile
    {
        public AutoMapperToData()
        {
            CreateMap<Service, ServiceModel>().ReverseMap();
            CreateMap<ServiceToLead, ServiceToLeadModel>().ReverseMap();
        }
    }
}
