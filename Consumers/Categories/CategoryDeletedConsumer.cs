using CatalogService.Contracts.Category.Events;
using MassTransit;

namespace ApiGateway.Consumers.Categories;

public class CategoryDeletedConsumer : IConsumer<CategoryDeletedEvent>
{
    private readonly ILogger<CategoryDeletedConsumer> _logger;

    public CategoryDeletedConsumer(ILogger<CategoryDeletedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CategoryDeletedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received CategoryDeletedEvent {@Message}", message);
    }
}