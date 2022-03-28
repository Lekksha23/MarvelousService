using AutoMapper;
using MarvelousService.API.Models;
using MarvelousService.BusinessLayer.Models;

namespace MarvelousService.API.Configuration
{
    public class AutoMapperFromApi : Profile
    {
        public AutoMapperFromApi()
        {
            CreateMap<AuthRequest, AuthModel>().ReverseMap(); 
            CreateMap<ServiceInsertRequest, ServiceModel>().ReverseMap();
            CreateMap<ServiceResponse, ServiceModel>().ReverseMap();
            CreateMap<ServiceToLeadInsertRequest, ServiceToLeadModel>().ReverseMap();
            CreateMap<LeadInsertRequest, LeadModel>().ReverseMap();
        }
    }
}
