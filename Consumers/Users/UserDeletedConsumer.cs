using CustomerService.Contracts.User.Events;
using MassTransit;

namespace ApiGateway.Consumers.Users;

public class UserDeletedConsumer : IConsumer<UserDeletedEvent>
{
    private readonly ILogger<UserDeletedConsumer> _logger;

    public UserDeletedConsumer(ILogger<UserDeletedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<UserDeletedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received UserDeletedEvent {@Message}", message);
    }
}