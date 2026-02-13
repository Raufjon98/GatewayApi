using CatalogService.Contracts.FoodCategory.Events;
using MassTransit;

namespace ApiGateway.Consumers.FoodCategories;

public class FoodCategoryCreatedConsumer : IConsumer<FoodCategoryCreatedEvent>
{
    private readonly ILogger<FoodCategoryCreatedConsumer> _logger;

    public FoodCategoryCreatedConsumer(ILogger<FoodCategoryCreatedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<FoodCategoryCreatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received FoodCategoryCreatedEvent {@Message}", message);
    }
}