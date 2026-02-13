using System.Text.Json;
using CatalogService.Contracts.FoodCategory.Events;
using CatalogService.Contracts.Interfaces;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;

namespace ApiGateway.Consumers.FoodCategories;

public class FoodCategoryCreatedConsumer : IConsumer<FoodCategoryCreatedEvent>
{
    private readonly IFoodCategoryService _categoryService;
    private readonly IDistributedCache _cache;

    public FoodCategoryCreatedConsumer(IFoodCategoryService categoryService, IDistributedCache cache)
    {
        _categoryService = categoryService;
        _cache = cache;
    }

    public async Task Consume(ConsumeContext<FoodCategoryCreatedEvent> context)
    {
        var key = $"foodCategory:{context.Message.Id}";
        var foodcategory = await _categoryService.GetFoodCategoryAsync(context.Message.Id);
        await _cache.SetStringAsync(key,
            JsonSerializer.Serialize(foodcategory),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(10)
            });
    }
}