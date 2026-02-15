using CustomerService.Contracts.User.Events;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;

namespace ApiGateway.Consumers.Users;

public class UserDeletedConsumer : IConsumer<UserDeletedEvent>
{
    private readonly IDistributedCache _cache;

    public UserDeletedConsumer(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task Consume(ConsumeContext<UserDeletedEvent> context)
    {
       var key = $"user:{context.Message.Id}";
       await _cache.RemoveAsync(key);
    }
}