using System.Text.Json;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using PaymentService.Contracts.Account.Events;
using PaymentService.Contracts.Interfaces;

namespace ApiGateway.Consumers.Accounts;

public class AccountCreatedConsumer : IConsumer<AccountCreatedEvent>
{
    private readonly IDistributedCache _cache;
    private readonly IAccountService _accountService;
    public AccountCreatedConsumer(IDistributedCache cache, IAccountService accountService)
    {
        _cache = cache;
        _accountService = accountService;
    }

    public async Task Consume(ConsumeContext<AccountCreatedEvent> context)
    {
        var key = $"user:{context.Message.Id}";
    }
}