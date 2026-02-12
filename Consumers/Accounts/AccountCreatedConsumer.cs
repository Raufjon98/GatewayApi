using MassTransit;
using PaymentService.Contracts.Account.Events;

namespace ApiGateway.Consumers.Accounts;

public class AccountCreatedConsumer : IConsumer<AccountCreatedEvent>
{
    private readonly ILogger<AccountCreatedConsumer> _logger;

    public AccountCreatedConsumer(ILogger<AccountCreatedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<AccountCreatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received AccountCreatedEvent {@Message}", message);
    }
}