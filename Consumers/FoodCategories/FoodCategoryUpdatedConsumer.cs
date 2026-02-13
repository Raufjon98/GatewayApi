using CatalogService.Contracts.FoodCategory.Events;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;

namespace ApiGateway.Consumers.FoodCategories;

public class FoodCategoryUpdatedConsumer : IConsumer<FoodCategoryUpdatedEvent>
{
    private readonly IDistributedCache _cache;

    public FoodCategoryUpdatedConsumer(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task Consume(ConsumeContext<FoodCategoryUpdatedEvent> context)
    {
        var key = $"foodCategory:{context.Message.Id}";
        await _cache.RemoveAsync(key);
    }
}