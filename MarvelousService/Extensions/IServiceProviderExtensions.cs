using MarvelousService.API.Configuration;
using MarvelousService.BusinessLayer.Services;
using MarvelousService.BusinessLayer.Services.Interfaces;
using MarvelousService.DataLayer.Interfaces;
using MarvelousService.DataLayer.Repositories;

namespace MarvelousService.API.Extensions
{
    public static class IServiceProviderExtensions
    {
        public static void RegisterMarvelousServiceRepositories(this IServiceCollection services)
        {
            services.AddScoped<IServiceRepository, ServiceRepository>();
        }

        public static void RegisterMarvelousServiceServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceService, ServiceService>();
        }

        public static void RegisterMarvelousServiceAutomappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperFromApi), typeof(AutoMapperToData));
        }
    }
}
