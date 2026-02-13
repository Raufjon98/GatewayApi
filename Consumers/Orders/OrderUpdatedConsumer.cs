using MassTransit;
using OrderService.Contracts.Order.Events;

namespace ApiGateway.Consumers.Orders;

public class OrderUpdatedConsumer : IConsumer<OrderUpdatedEvent>
{
    private readonly ILogger<OrderUpdatedConsumer> _logger;

    public OrderUpdatedConsumer(ILogger<OrderUpdatedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderUpdatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received OrderUpdatedEvent {@Message}", message);
    }
}