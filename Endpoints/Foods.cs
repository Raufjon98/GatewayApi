using ApiGateway.Extensions;
using CatalogService.Contracts.Food.Requests;
using CatalogService.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Endpoints;

public class Foods : EndpointGroupBase
{
    public override string Prefix => "foods";
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(Prefix);
        group.MapGet("/", GetAll);
        group.MapGet("/{foodId}", GetById);
        group.MapGet("/category/{foodCategoryId}", GetByFoodCategory);
        group.MapGet("/restaurant/{restaurantId}", GetByRestaurant);
        group.MapGet("/by-price", GetByPriceRange);
        group.MapPost("/", Create);
        group.MapPut("/{foodId}", Update);
        group.MapPut("/{foodId}/{isAvailable}", UpdateAvailability);
        group.MapPost("/delete/{foodId}", Delete);
    }

    public async Task<IResult> GetAll([FromServices] IFoodService foodService)
    {
        var result = await foodService.GetAllFoodsAsync();
        return Results.Ok(result);
    }

    public async Task<IResult> GetById([FromServices] IFoodService foodService, [FromQuery] string foodId)
    {
        var result = await foodService.GetFoodAsync(foodId);
        return Results.Ok(result);
    }

    public async Task<IResult> Create([FromServices] IFoodService foodService, [FromBody] CreateFoodRequest food)
    {
        var result = await foodService.CreateFoodAsync(food);
        return Results.Ok(result);
    }

    public async Task<IResult> Update([FromServices] IFoodService foodService, [FromQuery] string foodId,
        [FromBody] CreateFoodRequest food)
    {
        var result = await foodService.UpdateFoodAsync(foodId, food);
        return Results.Ok(result);
    }

    public async Task<IResult> UpdateAvailability([FromServices] IFoodService foodService, [FromQuery] string foodId,
        [FromQuery] bool isAvailable)
    {
        var result = await foodService.UpdateFoodAvailabilityAsync(foodId, isAvailable);
        return Results.Ok(result);
    }

    public async Task<IResult> Delete([FromServices] IFoodService foodService, [FromQuery] string foodId)
    {
        var result = await foodService.DeleteFoodAsync(foodId);
        return Results.Ok(result);
    }

    public async Task<IResult> GetByPriceRange(decimal min, decimal max, [FromServices] IFoodService foodService)
    {
        var result = await foodService.GetFoodsByPriceRangeAsync(min, max);
        return Results.Ok(result);
    }

    public async Task<IResult> GetByFoodCategory([FromServices] IFoodService foodService,
        [FromQuery] string foodCategoryId)
    {
        var result = await foodService.GetFoodsByCategoryAsync(foodCategoryId);
        return Results.Ok(result);
    }

    public async Task<IResult> GetByRestaurant([FromRoute] string restaurantId, [FromServices] IFoodService foodService)
    {
        var result = await foodService.GetFoodsByRestaurantAsync(restaurantId);
        return Results.Ok(result);
    }
}