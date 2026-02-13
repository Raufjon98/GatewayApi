using CatalogService.Contracts.FoodCategory.Events;
using MassTransit;

namespace ApiGateway.Consumers.FoodCategories;

public class FoodCategoryDeletedConsumer : IConsumer<FoodCategoryDeletedEvent>
{
    private readonly ILogger<FoodCategoryDeletedConsumer> _logger;

    public FoodCategoryDeletedConsumer(ILogger<FoodCategoryDeletedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<FoodCategoryDeletedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received FoodCategoryDeletedEvent {@Message}", message);
    }
}