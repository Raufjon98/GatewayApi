using System.Text.Json;
using ApiGateway.Extensions;
using ApiGateway.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using OrderService.Contracts.Cart.Requests;
using OrderService.Contracts.Cart.Responses;
using OrderService.Contracts.CartItem.Requests;
using OrderService.Contracts.Interfaces;

namespace ApiGateway.Endpoints;

public class Carts : EndpointGroupBase
{
    public override string Prefix => "carts";
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(Prefix).RequireAuthorization();
        group.MapPost("/", Create);
        group.MapGet("/", GetCart);
        group.MapPost("/addItem", AddItem);
        group.MapPost("/removeItem", RemoveItem);
    }

    public async Task<IResult> Create([FromServices] ICartService cartService, 
        [FromServices] IUser user, 
        [FromBody] CreateCartRequest cartRequest)
    {
        if (!Guid.TryParse(user.Id, out var customerId))
        {
            return Results.BadRequest("Invalid customerId");
        }
        
        var result = await cartService.CreateCartAsync(customerId, cartRequest);
        return Results.Ok(result);
    }

    public async Task<IResult> GetCart(
        [FromServices] ICartService cartService, 
        [FromServices] IUser user,
        [FromServices] IDistributedCache cache)
    {
        if (!Guid.TryParse(user.Id, out var customerId))
        {
            return Results.BadRequest("Invalid customerId");
        }
        
        var key = $"cart:{customerId}";
        var cached = await cache.GetStringAsync(key);
        
        if (cached != null)
        {
            return Results.Ok(JsonSerializer.Deserialize<CartResponse>(cached));
        }
        
        var result = await cartService.GetCartAsync(customerId);
        await cache.SetStringAsync(key, JsonSerializer.Serialize(result),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
            });
        
        return Results.Ok(result);
    }

    public async Task<IResult> AddItem([FromServices] ICartService cartService,
        [FromServices] IUser user,
        [FromBody] CartItemsRequest request)
    {
        if (!Guid.TryParse(user.Id, out var customerId))
        {
            return Results.BadRequest("Invalid customerId");
        }
        
        var result = await cartService.AddItemToCartAsync(customerId, request);
        return Results.Ok(result);
    }
    
    public async Task<IResult> RemoveItem([FromServices] ICartService cartService,
        [FromServices] IUser user,
        [FromBody] CartItemsRequest request)
    {
        if (!Guid.TryParse(user.Id, out var customerId))
        {
            return Results.BadRequest("Invalid customerId");
        }
        
        var result = await cartService.RemoveItemFromCartAsync(customerId, request);
        return Results.Ok(result);
    }
}