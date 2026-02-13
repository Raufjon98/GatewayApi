using CatalogService.Contracts.Cuisine.Events;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;

namespace ApiGateway.Consumers.Cuisines;

public class CuisineDeletedConsumer : IConsumer<CuisineDeletedEvent>
{
    private readonly IDistributedCache _cache;

    public CuisineDeletedConsumer(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task Consume(ConsumeContext<CuisineDeletedEvent> context)
    {
        var key = $"cuisine:{context.Message.Id}";
        await _cache.RemoveAsync(key);
    }
}