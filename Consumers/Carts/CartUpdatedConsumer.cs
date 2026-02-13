using MassTransit;
using OrderService.Contracts.Cart.Events;

namespace ApiGateway.Consumers.Carts;

public class CartUpdatedConsumer : IConsumer<CartUpdatedEvent>
{
    private readonly ILogger<CartUpdatedConsumer> _logger;

    public CartUpdatedConsumer(ILogger<CartUpdatedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CartUpdatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received CartUpdatedEvent {@Message}", message);
    }
}