﻿using MarvelousService.API.Configuration;
using MarvelousService.BusinessLayer.Configuration;
using MarvelousService.BusinessLayer.Configurations;
using MarvelousService.BusinessLayer.Services;
using MarvelousService.BusinessLayer.Services.Interfaces;
using MarvelousService.DataLayer.Repositories;
using MarvelousService.DataLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Logging;
using MassTransit;
using MarvelousService.API.Consumer;


namespace MarvelousService.API.Extensions
{
    public static class IServiceProviderExtensions
    {
        public static void RegisterMarvelousServiceRepositories(this IServiceCollection services)
        {
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IServiceToLeadRepository, ServiceToLeadRepository>();
            services.AddScoped<IServicePaymentRepository, ServicePaymentRepository>();
        }

        public static void RegisterMarvelousServiceServices(this IServiceCollection services)
        {
            services.AddScoped<ICRMClient, CRMClient>();
            services.AddScoped<IServiceToService, ServiceToService>();
            services.AddScoped<IServicePaymentService, ServicePaymentService>();
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

        public static void RegisterSwaggerAuth(this IServiceCollection swagger)
        {
            swagger.AddSwaggerGen(opt =>
            {
                opt.EnableAnnotations();
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MyAPI",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Git Repository",
                        Url = new Uri("https://github.com/Lekksha23/MarvelousServic"),
                    }
                });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
        
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[]  {}
                    }
                });
            });
        }

        public static void AddCustomAuth(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = AuthOptions.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true
                    };
                });
            services.AddAuthorization();
        }

        public static void AddMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                //x.AddConsumer<>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq://80.78.240.16", hst =>
                    {
                        hst.Username("nafanya");
                        hst.Password("qwe!23");
                    });

                    cfg.Publish<IServiceExchangeModel>(p =>
                    {
                        p.BindAlternateExchangeQueue("alternate-exchange", "alternate-queue");
                    });


                });
            });
        }
    }
}
