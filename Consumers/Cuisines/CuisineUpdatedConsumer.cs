using CatalogService.Contracts.Cuisine.Events;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;

namespace ApiGateway.Consumers.Cuisines;

public class CuisineUpdatedConsumer : IConsumer<CuisineUpdatedEvent>
{
    private readonly IDistributedCache _cache;

    public CuisineUpdatedConsumer(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task Consume(ConsumeContext<CuisineUpdatedEvent> context)
    {
        var key = $"cuisine:{context.Message.Id}";
        await _cache.RemoveAsync(key);
    }
}