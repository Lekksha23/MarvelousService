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

            CreateMap<ResourceInsertRequest, ResourceModel>().ReverseMap();
            CreateMap<ResourceModel, ResourceResponse>().ReverseMap();
            CreateMap<ResourceSoftDeleteRequest, ResourceModel>().ReverseMap();
            CreateMap<LeadInsertRequest, LeadModel>().ReverseMap();

            CreateMap<LeadResourceInsertRequest, LeadResourceModel>().ReverseMap();
            CreateMap<LeadResourceModel, LeadResourceByPayDateResponse>().ReverseMap();
            CreateMap<LeadResourceModel, LeadResourceResponse>().ReverseMap();
        }
    }
}
