using CatalogService.Contracts.Restaurant.Events;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;

namespace ApiGateway.Consumers.Restaurants;

public class RestaurantUpdatedConsumer : IConsumer<RestaurantUpdatedEvent>
{
    private readonly ILogger<RestaurantUpdatedConsumer> _logger;
    private readonly IDistributedCache _cache;

    public RestaurantUpdatedConsumer(ILogger<RestaurantUpdatedConsumer> logger, IDistributedCache cache)
    {
        _logger = logger;
        _cache = cache;
    }

    public async Task Consume(ConsumeContext<RestaurantUpdatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received RestaurantUpdatedEvent {@Message}", message);
        var key = $"restaurant:{context.Message.Id}";
        await _cache.RemoveAsync(key);
    }
}