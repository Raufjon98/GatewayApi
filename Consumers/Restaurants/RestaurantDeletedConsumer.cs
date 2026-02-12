using CatalogService.Contracts.Restaurant.Events;
using MassTransit;

namespace ApiGateway.Consumers.Restaurants;

public class RestaurantDeletedConsumer : IConsumer<RestaurantDeletedEvent>
{
    private readonly ILogger<RestaurantDeletedConsumer> _logger;

    public RestaurantDeletedConsumer(ILogger<RestaurantDeletedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<RestaurantDeletedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received RestaurantDeletedEvent {@Message}", message);
    }
}