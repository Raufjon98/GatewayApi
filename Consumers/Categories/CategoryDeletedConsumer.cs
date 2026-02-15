using CatalogService.Contracts.Category.Events;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;

namespace ApiGateway.Consumers.Categories;

public class CategoryDeletedConsumer : IConsumer<CategoryDeletedEvent>
{
    private readonly IDistributedCache _cache;

    public CategoryDeletedConsumer(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task Consume(ConsumeContext<CategoryDeletedEvent> context)
    {
        var key = $"category:{context.Message.Id}";
        await _cache.RemoveAsync(key);
    }
}