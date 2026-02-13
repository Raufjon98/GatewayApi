using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using OrderService.Contracts.Order.Events;

namespace ApiGateway.Consumers.Orders;

public record OrderStatusChangedConsumer :  IConsumer<OrderStatusChangedEvent>
{
    private readonly IDistributedCache _cache;
    public OrderStatusChangedConsumer(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task Consume(ConsumeContext<OrderStatusChangedEvent> context)
    {
        var key = $"order:{context.Message.Id}";
        await _cache.RemoveAsync(key);
    }
}