using ApiGateway.Extensions;
using ApiGateway.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OrderService.Contracts.Cart.Requests;
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
        if (string.IsNullOrEmpty(user.Id))
        {
            return Results.Unauthorized();
        }
        
        var customerId = Guid.Parse(user.Id);
        var result = await cartService.CreateCartAsync(customerId, cartRequest);
        return Results.Ok(result);
    }

    public async Task<IResult> GetCart([FromServices] ICartService cartService, 
        [FromServices] IUser user)
    {
        if (string.IsNullOrEmpty(user.Id))
        {
            return Results.Unauthorized();
        }
        
        var customerId = Guid.Parse(user.Id);
        var result = await cartService.GetCartAsync(customerId);
        return Results.Ok(result);
    }

    public async Task<IResult> AddItem([FromServices] ICartService cartService,
        [FromServices] IUser user,
        [FromBody] CartItemsRequest request)
    {
        if (string.IsNullOrEmpty(user.Id))
        {
            return Results.Unauthorized();
        }
        
        var customerId = Guid.Parse(user.Id);
        var result = await cartService.AddItemToCartAsync(customerId, request);
        return Results.Ok(result);
    }
    
    public async Task<IResult> RemoveItem([FromServices] ICartService cartService,
        [FromServices] IUser user,
        [FromBody] CartItemsRequest request)
    {
        if (string.IsNullOrEmpty(user.Id))
        {
            return Results.Unauthorized();
        }
        
        var customerId = Guid.Parse(user.Id);
        var result = await cartService.RemoveItemFromCartAsync(customerId, request);
        return Results.Ok(result);
    }
}