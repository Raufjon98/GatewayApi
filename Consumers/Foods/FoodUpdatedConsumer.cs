using CatalogService.Contracts.Food.Events;
using MassTransit;

namespace ApiGateway.Consumers.Foods;

public class FoodUpdatedConsumer : IConsumer<FoodUpdatedEvent>
{
    private readonly ILogger<FoodUpdatedConsumer> _logger;

    public FoodUpdatedConsumer(ILogger<FoodUpdatedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<FoodUpdatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received FoodUpdatedEvent {@Message}", message);
    }
}