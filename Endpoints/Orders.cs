using System.Text.Json;
using ApiGateway.Extensions;
using ApiGateway.Interfaces;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using OrderService.Contracts.Enums;
using OrderService.Contracts.Interfaces;
using OrderService.Contracts.Order.Requests;
using OrderService.Contracts.Order.Responses;
using StackExchange.Redis;

namespace ApiGateway.Endpoints;

public class Orders : EndpointGroupBase
{
    public override string Prefix => "orders";

    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(Prefix).RequireAuthorization();
        group.MapPost("/", Create);
        group.MapPut("/{orderId}/cancel", Cancel);
        group.MapPut("/{orderId}/preparation", Preparation);
        group.MapPut("/{orderId}/ready", MarkAsReady);
        group.MapPut("/{orderId}/complete", CompleteOrder);
        group.MapGet("/{orderId}", GetById);
        group.MapGet("/", GetAll);
        group.MapGet("/status/{orderId}", GetOrderStatus);
        group.MapPost("/addItems", AddToOrder);
        group.MapPost("/removeItems", RemoveFromOrder);
    }

    public async Task<IResult> Create([FromServices] IOrderService orderService,
        [FromServices] IUser user)
    {
        if (!Guid.TryParse(user.Id, out var customerId))
        {
            return Results.BadRequest("Invalid customerId");
        }

        var result = await orderService.CreateOrderAsync(customerId);
        return Results.Ok(result);
    }

    public async Task<IResult> Cancel([FromServices] IOrderService orderService,
        [FromServices] IUser user,
        [FromRoute] string orderId)
    {
        if (!Guid.TryParse(orderId, out var id))
        {
            return Results.BadRequest("Invalid orderId");
        }

        if (!Guid.TryParse(user.Id, out var customerId))
        {
            return Results.BadRequest("Invalid customerId");
        }

        var result = await orderService.CancelOrderAsync(customerId, id);
        return Results.Ok(result);
    }

    public async Task<IResult> Preparation([FromServices] IOrderService orderService,
        [FromServices] IUser user,
        [FromRoute] string orderId)
    {
        if (!Guid.TryParse(user.Id, out var customerId))
        {
            return Results.BadRequest("Invalid customerId");
        }

        if (!Guid.TryParse(orderId, out var id))
        {
            return Results.BadRequest("Invalid orderId");
        }

        var result = await orderService.StartPreparationAsync(customerId, id);
        return Results.Ok(result);
    }

    public async Task<IResult> MarkAsReady([FromServices] IOrderService orderService,
        [FromServices] IUser user,
        [FromRoute] string orderId)
    {
        if (!Guid.TryParse(user.Id, out var customerId))
        {
            return Results.BadRequest("Invalid customerId");
        }

        if (!Guid.TryParse(orderId, out var id))
        {
            return Results.BadRequest("Invalid orderId");
        }

        var result = await orderService.MarkAsReadyAsync(customerId, id);
        return Results.Ok(result);
    }

    public async Task<IResult> CompleteOrder([FromServices] IOrderService orderService,
        [FromServices] IUser user,
        [FromRoute] string orderId)
    {
        if (!Guid.TryParse(user.Id, out var customerId))
        {
            return Results.BadRequest("Invalid customerId");
        }

        if (!Guid.TryParse(orderId, out var id))
        {
            return Results.BadRequest("Invalid orderId");
        }

        var result = await orderService.CompleteOrderAsync(customerId, id);
        return Results.Ok(result);
    }

    public async Task<IResult> GetById(
        [FromServices] IOrderService orderService,
        [FromServices] IDistributedCache cache,
        [FromServices] IUser user,
        [FromRoute] string orderId)
    {
        if (!Guid.TryParse(user.Id, out var customerId))
        {
            return Results.BadRequest("Invalid customerId");
        }

        if (!Guid.TryParse(orderId, out var id))
        {
            return Results.BadRequest("Invalid orderId");
        }
        var key = $"order:{orderId}";
        var cached = await cache.GetStringAsync(key);
        if (cached is not null)
        {
            return Results.Ok(JsonSerializer.Deserialize<OrderResponse>(cached));
        }
        var result = await orderService.GetOrderAsync(customerId, id);
        
        await cache.SetStringAsync(key, JsonSerializer.Serialize(result),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
            });
        
        return Results.Ok(result);
    }

    public async Task<IResult> GetAll([FromServices] IOrderService orderService,
        [FromServices] IUser user,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] OrderStatus? orderStatus = null,
        [FromQuery] DateTime? dateFrom = null,
        [FromQuery] DateTime? dateTo = null)
    {
        if (!Guid.TryParse(user.Id, out var customerId))
        {
            return Results.BadRequest("Invalid customerId");
        }

        var result = await orderService.GetOrdersAsync(customerId, page, pageSize, orderStatus, dateFrom, dateTo);
        return Results.Ok(result);
    }

    public async Task<IResult> GetOrderStatus([FromServices] IOrderService orderService,
        [FromServices] IUser user,
        [FromRoute] string orderId)
    {
        if (!Guid.TryParse(user.Id, out var customerId))
        {
            return Results.BadRequest("Invalid customerId");
        }

        if (!Guid.TryParse(orderId, out var id))
        {
            return Results.BadRequest("Invalid orderId");
        }

        var result = await orderService.GetOrderStatusAsync(customerId, id);
        return Results.Ok(result);
    }

    public async Task<IResult> AddToOrder([FromServices] IOrderService orderService,
        [FromServices] IUser user,
        [FromBody] AddToOrderRequest request)
    {
        if (!Guid.TryParse(user.Id, out var customerId))
        {
            return Results.BadRequest("Invalid customerId");
        }
        var result = await orderService.AddToOrderAsync(customerId, request);
        return Results.Ok(result);
    }
    
    public async Task<IResult> RemoveFromOrder([FromServices] IOrderService orderService,
        [FromServices] IUser user,
        [FromBody] RemoveFromOrderRequest request)
    {
        if (!Guid.TryParse(user.Id, out var customerId))
        {
            return Results.BadRequest("Invalid customerId");
        }
        
        var result = await orderService.RemoveFromOrderAsync(customerId, request);
        return Results.Ok(result);
    }
}