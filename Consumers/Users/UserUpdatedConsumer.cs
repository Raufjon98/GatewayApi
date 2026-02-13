using CustomerService.Contracts.User.Events;
using MassTransit;

namespace ApiGateway.Consumers.Users;

public class UserUpdatedConsumer : IConsumer<UserUpdatedEvent>
{
    private readonly ILogger<UserUpdatedConsumer> _logger;

    public UserUpdatedConsumer(ILogger<UserUpdatedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<UserUpdatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received UserUpdatedEvent {@Message}", message);
    }
}