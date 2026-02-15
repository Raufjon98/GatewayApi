using CatalogService.Contracts.Address.Events;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;

namespace ApiGateway.Consumers.Addresses;

public class AddressUpdatedConsumer : IConsumer<AddressUpdatedEvent>
{
    private readonly IDistributedCache _cache;

    public AddressUpdatedConsumer(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task Consume(ConsumeContext<AddressUpdatedEvent> context)
    {
        var key = $"address:{context.Message.Id}";
        await _cache.RemoveAsync(key);
    }
}