using System.Text.Json;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using OrderService.Contracts.Cart.Events;
using OrderService.Contracts.Interfaces;

namespace ApiGateway.Consumers.Carts;

public class CartCreatedConsumer : IConsumer<CartCreatedEvent>
{
    private readonly ICartService _cartService;
    private readonly IDistributedCache _cache;

    public CartCreatedConsumer(ICartService cartService, IDistributedCache cache)
    {
        _cartService = cartService;
        _cache = cache;
    }

    public async Task Consume(ConsumeContext<CartCreatedEvent> context)
    {
       var key = $"cart:{context.Message.CustomerId}";
       var cart = await _cartService.GetCartAsync(context.Message.CustomerId);
       await _cache.SetStringAsync(key,
           JsonSerializer.Serialize(cart),
           new DistributedCacheEntryOptions
           {
               AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
           });
    }
}