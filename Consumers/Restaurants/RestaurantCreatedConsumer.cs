using CatalogService.Contracts.Restaurant.Events;
using MassTransit;

namespace ApiGateway.Consumers.Restaurants;

public class RestaurantCreatedConsumer : IConsumer<RestaurantCreatedEvent>
{
    private readonly ILogger<RestaurantCreatedConsumer> _logger;

    public RestaurantCreatedConsumer(ILogger<RestaurantCreatedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<RestaurantCreatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received RestaurantCreatedEvent {@Message}", message);
    }
}