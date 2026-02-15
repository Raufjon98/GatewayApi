using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using PaymentService.Contracts.Account.Events;

namespace ApiGateway.Consumers.Accounts;

public class AccountDeletedConsumer : IConsumer<AccountDeletedEvent>
{
    private readonly IDistributedCache _cache;
    public AccountDeletedConsumer(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task Consume(ConsumeContext<AccountDeletedEvent> context)
    {
        var key = $"account:{context.Message.Id}";
        await _cache.RemoveAsync(key);
    }
}