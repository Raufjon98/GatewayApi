using CatalogService.Contracts.Address.Events;
using MassTransit;

namespace ApiGateway.Consumers.Addresses;

public class AddressUpdatedConsumer : IConsumer<AddressUpdatedEvent>
{
    private readonly ILogger<AddressUpdatedConsumer> _logger;

    public AddressUpdatedConsumer(ILogger<AddressUpdatedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<AddressUpdatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received AddressUpdatedEvent {@Message}", message);
    }
}