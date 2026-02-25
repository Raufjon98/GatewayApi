using System.Text.Json;
using CatalogService.Contracts.Category.Events;
using CatalogService.Contracts.Interfaces;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;

namespace ApiGateway.Consumers.Categories;

public class CategoryCreatedConsumer : IConsumer<CategoryCreatedEvent>
{
    private readonly ICategoryService _categoryService;
    private readonly IDistributedCache _cache;
    public CategoryCreatedConsumer(ICategoryService categoryService, IDistributedCache cache)
    {
        _categoryService = categoryService;
        _cache = cache;
    }

    public async Task Consume(ConsumeContext<CategoryCreatedEvent> context)
    {
        var key = $"category:{context.Message.Id}";
        var category = await _categoryService.GetCategoryAsync(context.Message.Id);
        await _cache.SetStringAsync(key,
            JsonSerializer.Serialize(category),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(12)
            });
    }
}
