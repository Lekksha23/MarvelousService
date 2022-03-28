using AutoMapper;
using MarvelousService.API.Models;
using MarvelousService.BusinessLayer.Models;

namespace MarvelousService.API.Configuration
{
    public class AutoMapperFromApi : Profile
    {
        public AutoMapperFromApi()
        {
            CreateMap<AuthRequest, AuthModel>(); 
            CreateMap<ServiceInsertRequest, ServiceModel>();
            CreateMap<ServiceResponse, ServiceModel>();
            CreateMap<ServiceToLeadInsertRequest, ServiceToLeadModel>();
            CreateMap<LeadInsertRequest, LeadModel>();
        }
    }
}
