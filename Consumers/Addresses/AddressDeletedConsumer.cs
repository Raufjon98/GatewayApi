using CatalogService.Contracts.Address.Events;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;

namespace ApiGateway.Consumers.Addresses;

public class AddressDeletedConsumer : IConsumer<AddressDeletedEvent>
{
    private readonly IDistributedCache _cache;

    public AddressDeletedConsumer(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task Consume(ConsumeContext<AddressDeletedEvent> context)
    {
        var key = $"address:{context.Message.Id}";
        await _cache.RemoveAsync(key);
    }
}