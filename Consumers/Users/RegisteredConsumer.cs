using CustomerService.Contracts.User.Events;
using MassTransit;

namespace ApiGateway.Consumers.Users;

public class RegisteredConsumer : IConsumer<RegisteredEvent>
{
    private readonly ILogger<RegisteredConsumer> _logger;

    public RegisteredConsumer(ILogger<RegisteredConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<RegisteredEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received RegisteredEvent {@Message}", message);
    }
}