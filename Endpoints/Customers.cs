using ApiGateway.Extensions;
using ApiGateway.Interfaces;
using Customer.Contracts.User.Requests;
using CustomerService.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Endpoints;

public class Customers : EndpointGroupBase
{
   public override string Prefix => "customers";
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(Prefix);
        group.MapGet("/", GetCustomers);
        group.MapGet("/{customerId}", GetCustomer);
        group.MapPut("/{customerId}", UpdateCustomer);
        group.MapPut("/updatePassword/{customerId}", UpdateCustomerPassword);
        group.MapPut("/delete", DeleteCustomer).RequireAuthorization();
    }

    public async Task<IResult> GetCustomers([FromServices] IUserService userService)
    {
        var resuls = await userService.GetUsersAsync();
        return Results.Ok(resuls);
    }
    public async Task<IResult> GetCustomer([FromServices] IUserService userService, string customerId)
    {
        var result = await userService.GetUserAsync(customerId);
        if (result == null)
        {
            return Results.NotFound();
        }
        return Results.Ok(result);
    }

    public async Task<IResult> UpdateCustomer([FromServices] IUserService userService,
        [FromRoute] string customerId,
        [FromBody] UpdateUserRequest customer)
    {
        var result = await userService.UpdateUserAsync(customer, customerId);
        return Results.Ok(result);
    }

    public async Task<IResult> UpdateCustomerPassword([FromServices] IUserService userService,
        [FromRoute] string customerId, UpdateUserPasswordRequest request )
    {
        var result = await userService.UpdateUserPassword(customerId, request.OldPassword, request.NewPassword);
        return Results.Ok(result);
    }

    public async Task<IResult> DeleteCustomer([FromServices] IUserService userService,
        [FromServices] IUser user)
    {
        if (string.IsNullOrWhiteSpace(user.Id))
        {
            return Results.BadRequest("Invalid customerId");
        }
        await userService.DeleteUserAsync(user.Id);
        return Results.Ok("Customer was deleted successfully!");
    }
}