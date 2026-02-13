using CatalogService.Contracts.Food.Events;
using MassTransit;

namespace ApiGateway.Consumers.Foods;

public class FoodCreatedConsumer : IConsumer<FoodCreatedEvent>
{
    private readonly ILogger<FoodCreatedConsumer> _logger;

    public FoodCreatedConsumer(ILogger<FoodCreatedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<FoodCreatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received FoodCreatedEvent {@Message}", message);
    }
}