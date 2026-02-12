using CatalogService.Contracts.Restaurant.Events;
using MassTransit;

namespace ApiGateway.Consumers.Restaurants;

public class RestaurantUpdatedConsumer : IConsumer<RestaurantUpdatedEvent>
{
    private readonly ILogger<RestaurantUpdatedConsumer> _logger;

    public RestaurantUpdatedConsumer(ILogger<RestaurantUpdatedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<RestaurantUpdatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received RestaurantUpdatedEvent {@Message}", message);
    }
}