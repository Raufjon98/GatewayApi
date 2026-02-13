using MassTransit;
using PaymentService.Contracts.Account.Events;

namespace ApiGateway.Consumers.Accounts;

public class AccountUpdatedConsumer : IConsumer<AccountUpdatedEvent>
{
    private readonly ILogger<AccountUpdatedConsumer> _logger;

    public AccountUpdatedConsumer(ILogger<AccountUpdatedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<AccountUpdatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received AccountUpdatedEvent {@Message}", message);
    }
}