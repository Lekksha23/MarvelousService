using AutoMapper;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.DataLayer.Entities;

namespace MarvelousService.BusinessLayer.Configurations
{
    public class AutoMapperToData : Profile
    {
        public AutoMapperToData()
        {
            CreateMap<Resource, ResourceModel>().ReverseMap();
            CreateMap<LeadResource, LeadResourceModel>().ReverseMap();
            CreateMap<ResourcePayment, ResourcePaymentModel>().ReverseMap();
        }
    }
}
