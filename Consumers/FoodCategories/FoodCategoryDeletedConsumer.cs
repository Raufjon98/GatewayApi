using CatalogService.Contracts.FoodCategory.Events;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;

namespace ApiGateway.Consumers.FoodCategories;

public class FoodCategoryDeletedConsumer : IConsumer<FoodCategoryDeletedEvent>
{
    private readonly IDistributedCache _cache;

    public FoodCategoryDeletedConsumer(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task Consume(ConsumeContext<FoodCategoryDeletedEvent> context)
    {
        var key = $"foodCategory:{context.Message.Id}";
        await _cache.RemoveAsync(key);
    }
}