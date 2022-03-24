using AutoMapper;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.DataLayer.Entities;

namespace MarvelousService.BusinessLayer.Configurations
{
    public class AutoMapperToData : Profile
    {
        public AutoMapperToData()
        {
            CreateMap<Service, ServiceModel>().ReverseMap();
            CreateMap<ServiceToLead, ServiceToLeadModel>().ReverseMap();
            CreateMap<ServicePayment, ServicePaymentModel>().ReverseMap();
        }
    }
}
