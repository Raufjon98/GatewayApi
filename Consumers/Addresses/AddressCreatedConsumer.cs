using CatalogService.Contracts.Address.Events;
using MassTransit;

namespace ApiGateway.Consumers.Addresses;

public class AddressCreatedConsumer : IConsumer<AddressCreatedEvent>
{
    private readonly ILogger<AddressCreatedConsumer> _logger;

    public AddressCreatedConsumer(ILogger<AddressCreatedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<AddressCreatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received AddressCreatedEvent {@Message}", message);
    }
}