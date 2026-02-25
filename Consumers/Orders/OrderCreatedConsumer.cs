using System.Text.Json;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using OrderService.Contracts.Interfaces;
using OrderService.Contracts.Order.Events;

namespace ApiGateway.Consumers.Orders;

public record OrderCreatedConsumer :  IConsumer<OrderCreatedEvent>
{
    private readonly IDistributedCache _cache;
    private readonly IOrderService _orderService;

    public OrderCreatedConsumer(IDistributedCache cache, IOrderService orderService)
    {
        _cache = cache;
        _orderService = orderService;
    }

    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var key = $"order:{context.Message.Id}";
        var order = await _orderService.GetOrderAsync(context.Message.CustomerId, context.Message.Id);
        await _cache.SetStringAsync(key,
            JsonSerializer.Serialize(order),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });
    }
}