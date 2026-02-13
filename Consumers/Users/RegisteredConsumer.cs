using System.Text.Json;
using CustomerService.Contracts.Interfaces;
using CustomerService.Contracts.User.Events;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;

namespace ApiGateway.Consumers.Users;

public class RegisteredConsumer : IConsumer<RegisteredEvent>
{
    private readonly IDistributedCache _cache;
    private readonly IUserService _userService;

    public RegisteredConsumer(IDistributedCache cache, IUserService userService)
    {
        _cache = cache;
        _userService = userService;
    }

    public async Task Consume(ConsumeContext<RegisteredEvent> context)
    {
        var key = $"user:{context.Message.Id}";
        var user = await _userService.GetUserAsync(context.Message.Id);

        await _cache.SetStringAsync(key,
            JsonSerializer.Serialize(user),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            });
    }
}