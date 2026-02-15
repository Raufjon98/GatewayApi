using CatalogService.Contracts.Food.Events;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;

namespace ApiGateway.Consumers.Foods;

public class FoodDeletedConsumer : IConsumer<FoodDeletedEvent>
{
    private readonly IDistributedCache _cache;

    public FoodDeletedConsumer(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task Consume(ConsumeContext<FoodDeletedEvent> context)
    {
        var key = $"food:{context.Message.Id}";
        await _cache.RemoveAsync(key);
    }
}