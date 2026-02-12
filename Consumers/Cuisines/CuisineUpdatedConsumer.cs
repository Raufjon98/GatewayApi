using CatalogService.Contracts.Cuisine.Events;
using MassTransit;

namespace ApiGateway.Consumers.Cuisines;

public class CuisineUpdatedConsumer : IConsumer<CuisineUpdatedEvent>
{
    private readonly ILogger<CuisineUpdatedConsumer> _logger;

    public CuisineUpdatedConsumer(ILogger<CuisineUpdatedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CuisineUpdatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received CuisineUpdatedEvent {@Message}", message);
    }
}