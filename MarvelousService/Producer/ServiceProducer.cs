using Marvelous.Contracts.ExchangeModels;
using MarvelousService.API.Producer.Interface;
using MarvelousService.BusinessLayer;
using MarvelousService.BusinessLayer.Services.Interfaces;
using MassTransit;

namespace MarvelousService.API.Producer
{
    public class ServiceProducer : IServiceProducer
    {
        private readonly IServiceToLeadService _serviceToLead;
        private readonly ILogger<ServiceProducer> _logger;

        public ServiceProducer(IServiceToLeadService serviceToLead,ILogger<ServiceProducer> logger)          
        {
            _serviceToLead = serviceToLead;

            _logger = logger;
        }

        public async Task Main(int id)
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
                var serviceLead = await _serviceToLead.GetServiceToLeadById(id);

                await busControl.Publish<ITransactionExchangeModel>(new
                {
                    serviceLead.Capacity,
                  

                });

                _logger.LogInformation("Published");
            }

            finally
            {
                await busControl.StopAsync();
            }
        }


    }
}
