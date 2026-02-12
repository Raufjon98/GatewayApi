using CatalogService.Contracts.Food.Events;
using MassTransit;

namespace ApiGateway.Consumers.Foods;

public class FoodDeletedConsumer : IConsumer<FoodDeletedEvent>
{
    private readonly ILogger<FoodDeletedConsumer> _logger;

    public FoodDeletedConsumer(ILogger<FoodDeletedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<FoodDeletedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received FoodDeletedEvent {@Message}", message);
    }
}