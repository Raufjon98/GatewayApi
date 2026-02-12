using System.Text.RegularExpressions;
using ApiGateway.Extensions;
using ApiGateway.Interfaces;
using CustomerService.Contracts.Authorization.Requests;
using CustomerService.Contracts.Authorization.Responses;
using CustomerService.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Contracts.Interfaces;

namespace ApiGateway.Endpoints;

public class Auth : EndpointGroupBase
{
    public override string Prefix => "auth";
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(Prefix);
        group.MapPost("/login", Login);
        group.MapPost("/register", Register);
        group.MapPost("/logout", Logout).RequireAuthorization();
        group.MapPost("/delete", DeleteAccount).RequireAuthorization();
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

    public async Task<IResult> Logout([FromServices] IAuthService authService)
    {
        var result = await authService.LogoutAsync();
        if (!result)
        {
            return Results.BadRequest("Logout failed");
        }
        return Results.Ok("Customer logged out");
    }

    public async Task<IResult> DeleteAccount([FromServices] IUser user,
        [FromServices] IUserService userService)
    {
        if (string.IsNullOrWhiteSpace(user.Id))
        {
            return Results.BadRequest("Invalid customer Id");
        }
        var result = await userService.DeleteUserAsync(user.Id);
        if (result)
        {
            return Results.Ok("Your account has been deleted");
        }
        return Results.BadRequest("Failed to delete account");
    }
}
