using ApiGateway.Extensions;
using Contracts.Authorization.Requests;
using Contracts.Authorization.Responses;
using CustomerApi.MagicOnion.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Npgsql.Replication.PgOutput.Messages;

namespace ApiGateway.Endpoints;

public class Auth : EndpointGroupBase
{
    public override string Prefix => "auth";
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(Prefix);
        group.MapPost("/login", Login);
        group.MapPost("/register", Register);
    }

    public async Task<LoginResponse> Login([FromServices] IAuthService authService, [FromBody] LoginRequest request)
    {
        var result = await authService.LoginAsync(request);
        return result;
    }

    public async Task<IResult> Register([FromServices] IAuthService authService, [FromBody] RegisterRequest request)
    {
        var result = await authService.RegisterAsync(request);
        if (!result)
        {
            return Results.BadRequest("Registration failed");
        }
        return Results.Ok("Customer registered successfully!");
    }
}
