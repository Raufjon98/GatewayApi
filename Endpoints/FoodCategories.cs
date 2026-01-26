using ApiGateway.Extensions;
using CatalogService.Contracts.DTOs;
using CatalogService.MagicOnion.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Endpoints;

public class FoodCategories : EndpointGroupBase
{
    public override string Prefix => "foodcategories";
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(Prefix);
        group.MapGet("/", GetAll);
        group.MapGet("/{foodCategoryId}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/foodCategoryId", Update);
        group.MapDelete("/foodCategoryId", Delete);
    }

    public async Task<IResult> GetAll([FromServices] IFoodCategoryService categoryService)
    {
        var result = await categoryService.GetFoodCategoriesAsync();
        return Results.Ok(result);
    }

    public async Task<IResult> GetById([FromServices] IFoodCategoryService categoryService, [FromQuery] string id)
    {
        var result = await categoryService.GetFoodCategoryAsync(id);
        return Results.Ok(result);
    }

    public async Task<IResult> Create([FromServices] IFoodCategoryService categoryService, [FromBody] CreateFoodCategoryDto foodCategory)
    {
        var result = await categoryService.CreateFoodCategoryAsync(foodCategory);
        return Results.Ok(result);
    }

    public async Task<IResult> Update([FromServices] IFoodCategoryService categoryService,
        [FromQuery] string foodCategoryId, [FromBody] CreateFoodCategoryDto foodCategory)
    {
        var result = await categoryService.UpdateFoodCategoryAsync(foodCategoryId, foodCategory);
        return Results.Ok(result);
    }

    public async Task<IResult> Delete([FromServices] IFoodCategoryService categoryService, [FromQuery] string id)
    {
        var result = await categoryService.DeleteFoodCategoryAsync(id);
        return Results.Ok(result);
    }
}