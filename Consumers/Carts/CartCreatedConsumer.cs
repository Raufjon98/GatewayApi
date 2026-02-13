using MassTransit;
using OrderService.Contracts.Cart.Events;

namespace ApiGateway.Consumers.Carts;

public class CartCreatedConsumer : IConsumer<CartCreatedEvent>
{
    private readonly ILogger<CartCreatedConsumer> _logger;

    public CartCreatedConsumer(ILogger<CartCreatedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CartCreatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received CartCreatedEvent {@Message}", message);
    }
}