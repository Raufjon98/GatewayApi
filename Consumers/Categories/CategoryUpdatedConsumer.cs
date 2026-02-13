using CatalogService.Contracts.Category.Events;
using MassTransit;

namespace ApiGateway.Consumers.Categories;

public class CategoryUpdatedConsumer : IConsumer<CategoryUpdatedEvent>
{
    private readonly ILogger<CategoryUpdatedConsumer> _logger;

    public CategoryUpdatedConsumer(ILogger<CategoryUpdatedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CategoryUpdatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received CategoryUpdatedEvent {@Message}", message);
    }
}