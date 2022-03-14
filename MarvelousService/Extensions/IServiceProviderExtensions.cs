using MarvelousService.API.Configuration;
using MarvelousService.BusinessLayer.Configurations;
using MarvelousService.BusinessLayer.Services;
using MarvelousService.BusinessLayer.Services.Interfaces;
using MarvelousService.DataLayer.Interfaces;
using MarvelousService.DataLayer.Repositories;
using MarvelousService.DataLayer.Repositories.Interfaces;

namespace MarvelousService.API.Extensions
{
    public static class IServiceProviderExtensions
    {
        public static void RegisterMarvelousServiceRepositories(this IServiceCollection services)
        {
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IServiceToLeadRepository, ServiceToLeadRepository>();
        }

        public static void RegisterMarvelousServiceServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IServiceToLeadService, ServiceToLeadService>();
        }

        public static void RegisterMarvelousServiceAutomappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperFromApi), typeof(AutoMapperToData));
        }
    }
}
