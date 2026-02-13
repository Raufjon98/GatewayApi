using CatalogService.Contracts.Category.Events;
using MassTransit;

namespace ApiGateway.Consumers.Categories;

public class CategoryCreatedConsumer : IConsumer<CategoryCreatedEvent>
{
    private readonly ILogger<CategoryCreatedConsumer> _logger;

    public CategoryCreatedConsumer(ILogger<CategoryCreatedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CategoryCreatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received CategoryCreatedEvent {@Message}", message);
    }
}
