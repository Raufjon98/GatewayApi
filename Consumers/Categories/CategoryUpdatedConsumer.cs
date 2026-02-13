using CatalogService.Contracts.Category.Events;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;

namespace ApiGateway.Consumers.Categories;

public class CategoryUpdatedConsumer : IConsumer<CategoryUpdatedEvent>
{
    private readonly IDistributedCache _cache;

    public CategoryUpdatedConsumer(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task Consume(ConsumeContext<CategoryUpdatedEvent> context)
    {
        var  key = $"category:{context.Message.Id}";
        await _cache.RemoveAsync(key);
    }
}