using Marvelous.Contracts.ExchangeModels;
using MarvelousService.API.Producer.Interface;
using MarvelousService.BusinessLayer.Clients.Interfaces;
using MassTransit;

namespace MarvelousService.API.Producer
{
    public class ResourceProducer : IResourceProducer
    {
        private readonly IBus _bus;
        private readonly IResourceService _resourceService;
        private readonly ILeadResourceService _leadResource;
        private readonly ILogger<ResourceProducer> _logger;

        public ResourceProducer(
            ILeadResourceService leadResource,
            ILogger<ResourceProducer> logger,
            IResourceService resourceService,
            IBus bus)
        {
            _leadResource = leadResource;
            _logger = logger;
            _resourceService = resourceService;
            _bus = bus;
        }

        public async Task NotifyResourceAdded(int id)
        {
            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            var resource = await _resourceService.GetResourceById(id);

            await _bus.Publish(new ServiceExchangeModel
            {
                Id = resource.Id,
                Name = resource.Name,
                Description = resource.Description,
                Price = resource.Price,
                IsDeleted = resource.IsDeleted
            }, source.Token);

            _logger.LogInformation("Resource published");
        }

        public async Task NotifyLeadResourceAdded(int id)
        {
            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            var leadResource = await _leadResource.GetById(id);

            await _bus.Publish(new LeadResourceExchangeModel
            {
                Id = leadResource.Id,
                Period = (int)leadResource.Period,
                Status = (int)leadResource.Status,
                LeadId = leadResource.LeadId,
                Price = leadResource.Price
            }, source.Token);

            _logger.LogInformation("Resource published");
        }
    }
}

