using CustomerService.Contracts.User.Events;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;

namespace ApiGateway.Consumers.Users;

public class UserUpdatedConsumer : IConsumer<UserUpdatedEvent>
{
    private readonly IDistributedCache _cache;
    public UserUpdatedConsumer(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task Consume(ConsumeContext<UserUpdatedEvent> context)
    {
        var  key = $"user:{context.Message.Id}";
        await _cache.RemoveAsync(key);
    }
}