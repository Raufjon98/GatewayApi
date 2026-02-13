using MassTransit;
using OrderService.Contracts.Order.Events;

namespace ApiGateway.Consumers.Orders;

public record OrderCreatedConsumer :  IConsumer<OrderCreatedEvent>
{
    private readonly ILogger<OrderCreatedConsumer> _logger;

    public OrderCreatedConsumer(ILogger<OrderCreatedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received OrderCreatedEvent {@Message}", message);
    }
}