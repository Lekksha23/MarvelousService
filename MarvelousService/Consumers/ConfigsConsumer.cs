using Marvelous.Contracts.Configurations;
using MassTransit;

public class ConfigsConsumer
{
    private readonly IConfiguration _config;
    private readonly ILogger<ConfigsConsumer> _logger;

    public ConfigsConsumer(ILogger<ConfigsConsumer> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }

    public Task Consume(ConsumeContext<ResourceCfg> context)
    {
        _logger.LogInformation($"Configuration {context.Message.Key} change value {_config[context.Message.Key]} to {context.Message.Value}");
        _config[context.Message.Key] = context.Message.Value;
        return Task.CompletedTask;
    }
}
