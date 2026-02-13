using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using OrderService.Contracts.Cart.Events;

namespace ApiGateway.Consumers.Carts;

public class CartUpdatedConsumer : IConsumer<CartUpdatedEvent>
{
    private readonly IDistributedCache _cache;
    public CartUpdatedConsumer(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task Consume(ConsumeContext<CartUpdatedEvent> context)
    {
        var key =  $"cart:{context.Message.CustomerId}";
        await _cache.RemoveAsync(key);
    }
}