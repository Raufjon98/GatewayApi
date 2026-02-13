using System.Text.Json;
using CatalogService.Contracts.Food.Events;
using CatalogService.Contracts.Interfaces;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;

namespace ApiGateway.Consumers.Foods;

public class FoodCreatedConsumer : IConsumer<FoodCreatedEvent>
{
    private readonly IDistributedCache _cache;
    private readonly IFoodService _foodService;

    public FoodCreatedConsumer(IDistributedCache cache, IFoodService foodService)
    {
        _cache = cache;
        _foodService = foodService;
    }

    public async Task Consume(ConsumeContext<FoodCreatedEvent> context)
    {
        var key = $"food:{context.Message.Id}";
        var food = await _foodService.GetFoodAsync(context.Message.Id);
        await _cache.SetStringAsync(key, JsonSerializer.Serialize(food),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
            });
    }
}