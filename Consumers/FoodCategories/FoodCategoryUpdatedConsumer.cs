using CatalogService.Contracts.FoodCategory.Events;
using MassTransit;

namespace ApiGateway.Consumers.FoodCategories;

public class FoodCategoryUpdatedConsumer : IConsumer<FoodCategoryUpdatedEvent>
{
    private readonly ILogger<FoodCategoryUpdatedConsumer> _logger;

    public FoodCategoryUpdatedConsumer(ILogger<FoodCategoryUpdatedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<FoodCategoryUpdatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received FoodCategoryUpdatedEvent {@Message}", message);
    }
}