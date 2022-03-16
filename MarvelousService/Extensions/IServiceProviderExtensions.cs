﻿using MarvelousService.API.Configuration;
using MarvelousService.BusinessLayer.Configurations;
using MarvelousService.BusinessLayer.Services;
using MarvelousService.BusinessLayer.Services.Interfaces;
using MarvelousService.DataLayer.Interfaces;
using MarvelousService.DataLayer.Repositories;
using MarvelousService.DataLayer.Repositories.Interfaces;
using NLog.Extensions.Logging;

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
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IServiceToService, ServiceToService>();
            services.AddScoped<IServiceToLeadService, ServiceToLeadService>();
        }

        public static void RegisterMarvelousServiceAutomappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperFromApi), typeof(AutoMapperToData));
        }

        public static void RegisterLogger(this IServiceCollection service, IConfiguration config)
        {
            service.Configure<ConsoleLifetimeOptions>(opts => opts.SuppressStatusMessages = true);
            service.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.SetMinimumLevel(LogLevel.Information);
                loggingBuilder.AddNLog(config);
            });
        }
    }
}
