using System.Text.Json;
using ApiGateway.Extensions;
using CatalogService.Contracts.FoodCategory.Requests;
using CatalogService.Contracts.FoodCategory.Responses;
using CatalogService.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

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
        group.MapPost("/delete/foodCategoryId", Delete);
    }

    public async Task<IResult> GetAll([FromServices] IFoodCategoryService categoryService)
    {
        var result = await categoryService.GetFoodCategoriesAsync();
        return Results.Ok(result);
    }

    public async Task<IResult> GetById(
        [FromServices] IFoodCategoryService categoryService, 
        [FromServices] IDistributedCache cache,
        [FromQuery] string id)
    {
        var key = $"foodcategory:{id}";
        var cached = await cache.GetStringAsync(key);
        if (cached is not null)
        {
            return Results.Ok(JsonSerializer.Deserialize<FoodCategoryResponse>(cached));
        }
        
        var result = await categoryService.GetFoodCategoryAsync(id);
        await cache.SetStringAsync(key, JsonSerializer.Serialize(result),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
            });
        return Results.Ok(result);
    }

    public async Task<IResult> Create([FromServices] IFoodCategoryService categoryService, [FromBody] CreateFoodCategoryRequest foodCategory)
    {
        var result = await categoryService.CreateFoodCategoryAsync(foodCategory);
        return Results.Ok(result);
    }

    public async Task<IResult> Update([FromServices] IFoodCategoryService categoryService,
        [FromQuery] string foodCategoryId, [FromBody] CreateFoodCategoryRequest foodCategory)
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