using CatalogService.Contracts.Cuisine.Events;
using MassTransit;

namespace ApiGateway.Consumers.Cuisines;

public class CuisineDeletedConsumer : IConsumer<CuisineDeletedEvent>
{
    private readonly ILogger<CuisineDeletedConsumer> _logger;

    public CuisineDeletedConsumer(ILogger<CuisineDeletedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CuisineDeletedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received CuisineDeletedEvent {@Message}", message);
    }
}