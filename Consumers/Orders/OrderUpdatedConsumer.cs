using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using OrderService.Contracts.Order.Events;

namespace ApiGateway.Consumers.Orders;

public class OrderUpdatedConsumer : IConsumer<OrderUpdatedEvent>
{
    private readonly ILogger<OrderUpdatedConsumer> _logger;
    private readonly IDistributedCache _cache;

    public OrderUpdatedConsumer(ILogger<OrderUpdatedConsumer> logger, IDistributedCache cache)
    {
        _logger = logger;
        _cache = cache;
    }

    public async Task Consume(ConsumeContext<OrderUpdatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received OrderUpdatedEvent {@Message}", message);
        var key = $"order:{context.Message.Id}";
        await _cache.RemoveAsync(key);
    }
}