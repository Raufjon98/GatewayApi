using CatalogService.Contracts.Food.Events;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;

namespace ApiGateway.Consumers.Foods;

public class FoodUpdatedConsumer : IConsumer<FoodUpdatedEvent>
{
    private readonly IDistributedCache _cache;

    public FoodUpdatedConsumer(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task Consume(ConsumeContext<FoodUpdatedEvent> context)
    {
        var key = $"food:{context.Message.Id}";
        await _cache.RemoveAsync(key);
    }
}