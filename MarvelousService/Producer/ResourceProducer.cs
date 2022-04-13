using Marvelous.Contracts.ExchangeModels;
using MarvelousService.API.Producer.Interface;
using MarvelousService.BusinessLayer.Clients.Interfaces;
using MassTransit;

namespace MarvelousService.API.Producer
{
    public class ResourceProducer : IResourceProducer
    {
        private readonly IResourceService _resourceService;
        private readonly ILeadResourceService _leadResource;
        private readonly ILogger<ResourceProducer> _logger;

        public ResourceProducer(ILeadResourceService leadResource, ILogger<ResourceProducer> logger, IResourceService resourceService)          
        {
            _leadResource = leadResource;
            _logger = logger;
            _resourceService = resourceService;
        }

        public async Task NotifyResourceAdded(int id)
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
                var resource = await _resourceService.GetResourceById(id);

                await busControl.Publish<ServiceExchangeModel>( new
                {
                    Id = resource.Id,
                    Name = resource.Name,
                    Description = resource.Description,
                    Price = resource.Price,
                    IsDeleted = resource.IsDeleted
                });
                
            }
            finally
            {                
                await busControl.StopAsync();
            }

            _logger.LogInformation("Resource published");
        }

        public async Task NotifyLeadResourceAdded(int id)
        {
            var busControlfromLead = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("rabbitmq://80.78.240.16", hst =>
                {
                    hst.Username("nafanya");
                    hst.Password("qwe!23");

                });

            });

            var sourceLead = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await busControlfromLead.StartAsync(sourceLead.Token);
            try
            {
                var  leadService = await _leadResource.GetByLeadId(id);

                await busControlfromLead.Publish<LeadResourceExchangeModel>(new
                {
                    //Id = leadService.Id,
                    //Period = leadService.Period,
                    //Status = leadService.Status,
                    //LeadId = leadService.LeadId,
                    //Price = leadService.Price

                });
                _logger.LogInformation("Resource published");

            }
            finally
            {
                await busControlfromLead.StopAsync();
            }

        }


    }
}
