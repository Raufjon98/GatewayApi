using MassTransit;
using OrderService.Contracts.Order.Events;

namespace ApiGateway.Consumers.Orders;

public record OrderStatusChangedConsumer :  IConsumer<OrderStatusChangedEvent>
{
    private readonly ILogger<OrderStatusChangedConsumer> _logger;

    public OrderStatusChangedConsumer(ILogger<OrderStatusChangedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderStatusChangedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received OrderStatusChangedEvent {@Message}", message);
    }
}