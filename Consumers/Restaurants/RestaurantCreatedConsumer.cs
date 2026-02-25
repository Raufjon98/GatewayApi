using System.Text.Json;
using CatalogService.Contracts.Interfaces;
using CatalogService.Contracts.Restaurant.Events;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;

namespace ApiGateway.Consumers.Restaurants;

public class RestaurantCreatedConsumer : IConsumer<RestaurantCreatedEvent>
{
    private readonly ILogger<RestaurantCreatedConsumer> _logger;
    private readonly IDistributedCache _cache;
    private readonly IRestaurantService _restaurantService;

    public RestaurantCreatedConsumer(ILogger<RestaurantCreatedConsumer> logger, IDistributedCache cache, IRestaurantService restaurantService)
    {
        _logger = logger;
        _cache = cache;
        _restaurantService = restaurantService;
    }

    public async Task Consume(ConsumeContext<RestaurantCreatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received RestaurantCreatedEvent {@Message}", message);
        var key = $"restaurant:{context.Message.Id}";
        var restaurant = await _restaurantService.GetRestaurantAsync(context.Message.Id);
        await _cache.SetStringAsync(key,
            JsonSerializer.Serialize(restaurant),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(3)
            });
    }
}