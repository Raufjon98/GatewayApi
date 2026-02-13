using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using PaymentService.Contracts.Account.Events;

namespace ApiGateway.Consumers.Accounts;

public class AccountUpdatedConsumer : IConsumer<AccountUpdatedEvent>
{
    private readonly IDistributedCache _cache;

    public AccountUpdatedConsumer(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task Consume(ConsumeContext<AccountUpdatedEvent> context)
    {
        var key = $"account:{context.Message.Id}";
        await _cache.RemoveAsync(key);
    }
}