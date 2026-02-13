using CatalogService.Contracts.Address.Events;
using MassTransit;

namespace ApiGateway.Consumers.Addresses;

public class AddressDeletedConsumer : IConsumer<AddressDeletedEvent>
{
    private readonly ILogger<AddressDeletedConsumer> _logger;

    public AddressDeletedConsumer(ILogger<AddressDeletedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<AddressDeletedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received AddressDeletedEvent {@Message}", message);
    }
}