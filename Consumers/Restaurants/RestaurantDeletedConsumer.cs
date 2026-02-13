using CatalogService.Contracts.Restaurant.Events;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;

namespace ApiGateway.Consumers.Restaurants;

public class RestaurantDeletedConsumer : IConsumer<RestaurantDeletedEvent>
{
    private readonly ILogger<RestaurantDeletedConsumer> _logger;
    private readonly IDistributedCache  _cache;

    public RestaurantDeletedConsumer(ILogger<RestaurantDeletedConsumer> logger, IDistributedCache cache)
    {
        _logger = logger;
        _cache = cache;
    }

    public async Task Consume(ConsumeContext<RestaurantDeletedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received RestaurantDeletedEvent {@Message}", message);
        var key = $"restaurant:{context.Message.Id}";
        await _cache.RemoveAsync(key);
    }
}