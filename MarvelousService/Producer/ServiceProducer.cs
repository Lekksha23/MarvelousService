using Marvelous.Contracts.ExchangeModels;
using MarvelousService.API.Producer.Interface;
using MarvelousService.BusinessLayer;
using MarvelousService.BusinessLayer.Services.Interfaces;
using MassTransit;

namespace MarvelousService.API.Producer
{
    public class ServiceProducer : IServiceProducer
    {
        private readonly IServiceToService _serviceToService;
        private readonly IServiceToLeadService _serviceToLead;
        private readonly ILogger<ServiceProducer> _logger;

        public ServiceProducer(IServiceToLeadService serviceToLead,ILogger<ServiceProducer> logger)          
        {
            _serviceToLead = serviceToLead;

            _logger = logger;
        }

        public async Task NotifyServiceAdded(int id)
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("rabbitmq://80.78.240.16", hst =>
                {
                    hst.Username("nafanya");
                    hst.Password("qwe!23");

                });

            });
             var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await busControl.StartAsync(source.Token);
            try
            {
                var service = await _serviceToService.GetServiceById(id);

                await busControl.Publish<ServiceExchangeModel>(new
                {
                    Id = service.Id,
                    Name = service.Name,
                    Description = service.Description,
                    Price = service.Price,
                    IsDeleted = service.IsDeleted
                });
                _logger.LogInformation("Service published");
            }
            finally
            {
                _logger.LogWarning("Service not published");
                await busControl.StopAsync();
            }

            _logger.LogInformation("Service published");
        }

        public async Task NotifyServiceToLeadAdded(int id)
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("rabbitmq://80.78.240.16", hst =>
                {
                    hst.Username("nafanya");
                    hst.Password("qwe!23");

                });

            });

            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await busControl.StartAsync(source.Token);
            try
            {
                var service = await _serviceToLead.GetServiceToLeadById(id);

                await busControl.Publish<ServiceExchangeModel>(new
                {
                   


                });
                _logger.LogInformation("Account published");

            }
            finally
            {
                _logger.LogWarning("Accoun not published");
                await busControl.StopAsync();
            }

        }


    }
}
