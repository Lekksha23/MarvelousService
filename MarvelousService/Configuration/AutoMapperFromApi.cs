using AutoMapper;
using MarvelousService.API.Models;
using MarvelousService.BusinessLayer.Models;

namespace MarvelousService.API.Configuration
{
    public class AutoMapperFromApi : Profile
    {
        public AutoMapperFromApi()
        {
            CreateMap<ResourceInsertRequest, ResourceModel>();
            CreateMap<ResourceModel, ResourceResponse>();
            CreateMap<ResourceSoftDeleteRequest, ResourceModel>();
            CreateMap<ResourceModel, ResourceForPayDateResponse>();
            
            CreateMap<LeadInsertRequest, LeadModel>();

            CreateMap<LeadResourceInsertRequest, LeadResourceModel>();
            CreateMap<LeadResourceModel, LeadResourceByPayDateResponse>();
            CreateMap<LeadResourceModel, LeadResourceResponse>();
        }
    }
}
