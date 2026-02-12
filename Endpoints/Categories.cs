using ApiGateway.Extensions;
using CatalogService.Contracts.Category.Requests;
using CatalogService.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Endpoints;

public class Categories : EndpointGroupBase
{
    public override string Prefix => "categories";
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(Prefix);
        group.MapGet("/", GetAll);
        group.MapGet("/{categoryId}", GetById);
        group.MapPost("/", Create).RequireAuthorization();
        group.MapPut("/{categoryId}", Update).RequireAuthorization();
        group.MapPost("/delete/{categoryId}", Delete).RequireAuthorization();
        
    }

    public async Task<IResult> GetAll([FromServices] ICategoryService categoryService)
    {
        var result = await categoryService.GetAllCategoriesAsync();
        return Results.Ok(result);
    }

    public async Task<IResult> GetById([FromServices] ICategoryService categoryService, [FromRoute] string categoryId)
    {
        var result = await categoryService.GetCategoryAsync(categoryId);
        return Results.Ok(result);
    }

    public async Task<IResult> Create([FromServices] ICategoryService categoryService,
        [FromBody] CreateCategoryRequest category)
    {
        var result = await categoryService.CreateCategoryAsync(category);
        return Results.Ok(result);
    }

    public async Task<IResult> Update([FromServices] ICategoryService categoryService, [FromRoute] string categoryId,
        [FromBody] CreateCategoryRequest category)
    {
        var result = await categoryService.UpdateCategoryAsync(categoryId, category);
        return Results.Ok(result);
    }

    public async Task<IResult> Delete([FromServices] ICategoryService categoryService, [FromRoute] string categoryId)
    {
        var result = await categoryService.DeleteCategoryAsync(categoryId);
        return Results.Ok(result);
    }
}