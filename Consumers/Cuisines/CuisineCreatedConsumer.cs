using CatalogService.Contracts.Cuisine.Events;
using MassTransit;

namespace ApiGateway.Consumers.Cuisines;

public class CuisineCreatedConsumer : IConsumer<CuisineCreatedEvent>
{
    private readonly ILogger<CuisineCreatedConsumer> _logger;

    public CuisineCreatedConsumer(ILogger<CuisineCreatedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CuisineCreatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received CuisineCreatedEvent {@Message}", message);
    }
}