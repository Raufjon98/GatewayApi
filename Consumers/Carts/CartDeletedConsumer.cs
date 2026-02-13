using MassTransit;
using OrderService.Contracts.Cart.Events;

namespace ApiGateway.Consumers.Carts;

public class CartDeletedConsumer : IConsumer<CartUpdatedEvent>
{
    private readonly ILogger<CartDeletedConsumer> _logger;

    public CartDeletedConsumer(ILogger<CartDeletedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CartUpdatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received CartDeletedEvent {@Message}", message);
    }
}