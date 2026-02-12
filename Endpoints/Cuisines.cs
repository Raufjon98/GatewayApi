using ApiGateway.Extensions;
using CatalogService.Contracts.Cuisine.Requests;
using CatalogService.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Endpoints;

public class Cuisines : EndpointGroupBase
{
    public override string Prefix => "cuisines";
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(Prefix);
        group.MapGet("/", GetAll);
        group.MapGet("/{cuisineId}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{cuisineId}", Update);
        group.MapPost("/delete/{cuisineId}", Delete);
    }

    public async Task<IResult> GetAll([FromServices] ICuisineService  cuisineService)
    {
        var result = await cuisineService.GetCuisinesAsync();
        return Results.Ok(result);
    }

    public async Task<IResult> GetById([FromServices] ICuisineService cuisineService, [FromQuery] string id)
    {
        var result = await cuisineService.GetCuisineAsync(id);
        return Results.Ok(result);
    }

    public async Task<IResult> Create([FromServices] ICuisineService cuisineService,
        [FromBody] CreateCuisineRequest cuisine)
    {
        var result = await cuisineService.CreateCuisineAsync(cuisine);
        return Results.Ok(result);
    }

    public async Task<IResult> Update([FromServices] ICuisineService cuisineService, [FromQuery] string id,
        [FromBody] CreateCuisineRequest cuisine)
    {
        var result = await cuisineService.UpdateCuisineAsync(id, cuisine);
        return Results.Ok(result);
    }

    public async Task<IResult> Delete([FromServices] ICuisineService cuisineService, [FromQuery] string id)
    {
        var result = await cuisineService.DeleteCuisineAsync(id);
        return Results.Ok(result);
    }
}
