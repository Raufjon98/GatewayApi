using System.Text.Json;
using CatalogService.Contracts.Address.Events;
using CatalogService.Contracts.Interfaces;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;

namespace ApiGateway.Consumers.Addresses;

public class AddressCreatedConsumer : IConsumer<AddressCreatedEvent>
{
    private readonly IDistributedCache _cache;
    private readonly IAddressService _addressService;

    public AddressCreatedConsumer(IDistributedCache cache, IAddressService addressService)
    {
        _cache = cache;
        _addressService = addressService;
    }

    public async Task Consume(ConsumeContext<AddressCreatedEvent> context)
    {
        var key = $"address:{context.Message.Id}";
        var address = await _addressService.GetAddressAsync(context.Message.Id);

        await _cache.SetStringAsync(key,
            JsonSerializer.Serialize(address),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            });
    }
}