using System.Text.Json;
using CatalogService.Contracts.Cuisine.Events;
using CatalogService.Contracts.Interfaces;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;

namespace ApiGateway.Consumers.Cuisines;

public class CuisineCreatedConsumer : IConsumer<CuisineCreatedEvent>
{
    private readonly IDistributedCache _cache;
    private readonly ICuisineService _cuisineService;
    public CuisineCreatedConsumer(IDistributedCache cache, ICuisineService cuisineService)
    {
        _cache = cache;
        _cuisineService = cuisineService;
    }

    public async Task Consume(ConsumeContext<CuisineCreatedEvent> context)
    {
        var key = $"cuisine:{context.Message.Id}";
        var cuisine = await _cuisineService.GetCuisineAsync(context.Message.Id);
        await _cache.SetStringAsync(key,
            JsonSerializer.Serialize(cuisine),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(10)
            });
    }
}