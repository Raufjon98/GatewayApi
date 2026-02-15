using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using OrderService.Contracts.Cart.Events;

namespace ApiGateway.Consumers.Carts;

public class CartRemovedConsumer : IConsumer<CartRemovedEvent>
{
    private readonly IDistributedCache _cache;

    public CartRemovedConsumer(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task Consume(ConsumeContext<CartRemovedEvent> context)
    {
        var key = $"cart:{context.Message.CustomerId}";
        await _cache.RemoveAsync(key);
    }
}