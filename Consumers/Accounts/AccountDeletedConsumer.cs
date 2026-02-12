using MassTransit;
using PaymentService.Contracts.Account.Events;

namespace ApiGateway.Consumers.Accounts;

public class AccountDeletedConsumer : IConsumer<AccountDeletedEvent>
{
    private readonly ILogger<AccountDeletedConsumer> _logger;

    public AccountDeletedConsumer(ILogger<AccountDeletedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<AccountDeletedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received AccountDeletedEvent {@Message}", message);
    }
}