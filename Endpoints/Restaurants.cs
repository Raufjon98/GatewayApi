using ApiGateway.Extensions;
using CatalogService.Contracts.DTOs;
using CatalogService.MagicOnion.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Endpoints;

public class Restaurants : EndpointGroupBase
{
    public override string Prefix => "restaurants";
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(Prefix);
        group.MapGet("/", GetAll);
        group.MapGet("/{restaurantId}", GetById);
        group.MapGet("/search", SearchByName);
        group.MapGet("/paged", GetPaged);
        group.MapGet("/cuisine/{cuisineId}", GetByCuisine);
        group.MapGet("/category/{categoryId}", GetByCategory);
        group.MapGet("/availables", GetAvailableRestaurants);
        group.MapGet("/actives", GetActiveRestaurants);
        group.MapPost("/", Create);
        group.MapPut("/{restaurantId}", Update);
        group.MapPut("/update-availability/{restaurantId}/{isAvailable}", UpdateAvailability);
        group.MapPut("/update-category/{restaurantId}/{categoryId}", UpdateCategory);
        group.MapDelete("/{RestaurantId}", Delete);
        group.MapDelete("/", DeleteInActives);
    }

    public async Task<IResult> GetAll([FromServices] IRestaurantService restaurantService)
    {
        var result = await restaurantService.GetAllRestaurantsAsync();
        return Results.Ok(result);
    }

    public async Task<IResult> GetById([FromServices] IRestaurantService restaurantService, [FromQuery] string id)
    {
        var result = await restaurantService.GetRestaurantAsync(id);
        return Results.Ok(result);
    }

    public async Task<IResult> SearchByName([FromServices] IRestaurantService restaurantService,
        string name)
    {
        var result = await restaurantService.SearchRestaurantByNameAsync(name);
        return Results.Ok(result);
    }

    public async Task<IResult> GetPaged(int page, int pageSize, [FromServices] IRestaurantService restaurantService)
    {
        var result = await restaurantService.GetPagedRestaurantsAsync(page, pageSize);
        return Results.Ok(result);
    }

    public async Task<IResult> GetByCuisine([FromServices] IRestaurantService restaurantService,
        [FromQuery] string cuisineId)
    {
        var result = await restaurantService.GetRestaurantByCuisineAsync(cuisineId);
        return Results.Ok(result);
    }

    public async Task<IResult> GetByCategory([FromServices] IRestaurantService restaurantService,
        [FromQuery] string categoryId)
    {
        var result = await restaurantService.GetRestaurantsByCategoryAsync(categoryId);
        return Results.Ok(result);
    }

    public async Task<IResult> GetAvailableRestaurants([FromServices] IRestaurantService restaurantService)
    {
        var result = await restaurantService.GetAvailableRestaurantsAsync();
        return Results.Ok(result);
    }

    public async Task<IResult> GetActiveRestaurants([FromServices] IRestaurantService restaurantService)
    {
        var result = await restaurantService.GetActiveRestaurantsAsync();
        return Results.Ok(result);
    }

    public async Task<IResult> Create([FromServices] IRestaurantService restaurantService,
        [FromBody] CreateRestaurantDto restaurant)
    {
        var result = await restaurantService.CreateRestaurantAsync(restaurant);
        return Results.Ok(result);
    }

    public async Task<IResult> Update([FromServices] IRestaurantService restaurantService,
        [FromBody] CreateRestaurantDto restaurant, [FromQuery] string restaurantId)
    {
        var result = await restaurantService.UpdateRestaurantAsync(restaurantId, restaurant);
        return Results.Ok(result);
    }

    public async Task<IResult> UpdateAvailability([FromServices] IRestaurantService restaurantService,
        [FromQuery] string restaurantId, [FromQuery] bool isAvailable)
    {
        var result = await restaurantService.UpdateRestauranAvailabilitytAsync(restaurantId, isAvailable);
        return Results.Ok(result);
    }

    public async Task<IResult> UpdateCategory([FromServices] IRestaurantService restaurantService,
        [FromQuery] string restaurantId, [FromQuery] string categoryId)
    {
        var result = await restaurantService.UpdateRestaurantCategoryAsync(restaurantId, categoryId);
        return Results.Ok(result);
    }

    public async Task<IResult> Delete([FromServices] IRestaurantService restaurantService,
        [FromQuery] string restaurantId)
    {
        var result = await restaurantService.DeleteRestaurantAsync(restaurantId);
        return Results.Ok(result);
    }

    public async Task<IResult> DeleteInActives([FromServices] IRestaurantService restaurantService)
    {
        var result = await restaurantService.DeleteInActiveRestaurantAsync();
        return Results.Ok(result);
    }
}