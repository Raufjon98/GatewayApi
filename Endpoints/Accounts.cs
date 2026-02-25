using ApiGateway.Extensions;
using ApiGateway.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Contracts.Account.Requests;
using PaymentService.Contracts.Interfaces;

namespace ApiGateway.Endpoints;

public class Accounts : EndpointGroupBase
{
    public override string Prefix => "accounts";

    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(Prefix).RequireAuthorization();
        group.MapPost("/", CreateAccount);
        group.MapPost("/top-up", TopUpBalance);
        group.MapPost("/withdraw", WithdrawBalance);
        group.MapPut("/update-status", UpdateStatus);
        group.MapGet("/balance/", GetBalance);
        group.MapGet("/top-ups/", GetTopUps);
        group.MapGet("/transactions/", GetTransactions);
        group.MapGet("/withdraws/", GetWithdraws);
        group.MapPost("/delete", DeleteAccount);
    }

    public async Task<IResult> CreateAccount([FromServices] IAccountService accountService,
        [FromServices] IUser user)
    {
        if (!Guid.TryParse(user.Id, out var customerId))
        {
            return Results.BadRequest("Invalid customerId");
        }
        
        var result = await accountService.CreateAccountAsync(customerId);
        return Results.Ok(result);
    }

    public async Task<IResult> TopUpBalance([FromServices] IAccountService accountService,
        [FromServices] IUser user,
        [FromBody] TopUpRequest topUpRequest)
    {
        if (!Guid.TryParse(user.Id, out var customerId))
        {
            return Results.BadRequest("Invalid customerId");
        }
        
        topUpRequest.CustomerId = customerId;
        var result = await accountService.TopUpBalanceAsync(topUpRequest);
        return Results.Ok(result);
    }

    public async Task<IResult> WithdrawBalance([FromServices] IAccountService accountService,
        [FromServices] IUser user,
        [FromBody] WithdrawRequest withdrawRequest)
    {
        if (!Guid.TryParse(user.Id, out var customerId))
        {
            return Results.BadRequest("Invalid customerId");
        }
        
        withdrawRequest.CustomerId = customerId;
        var result = await accountService.WithdrawBalanceAsync(withdrawRequest);
        return Results.Ok(result);
    }

    public async Task<IResult> UpdateStatus([FromServices] IAccountService accountService,
        [FromServices] IUser user,
        [FromBody] UpdateAccountStatusRequest updateAccountStatusRequest)
    {
        if (!Guid.TryParse(user.Id, out var customerId))
        {
            return Results.BadRequest("Invalid customerId");
        }
        
        updateAccountStatusRequest.CustomerId = customerId;
        var result = await accountService.UpdateAccountStatusAsync(updateAccountStatusRequest);
        return Results.Ok(result);
    }

    public async Task<IResult> GetBalance([FromServices] IAccountService accountService,
        [FromServices] IUser user)
    {
        if (!Guid.TryParse(user.Id, out var customerId))
        {
            return Results.BadRequest("Invalid customerId");
        }

        var result = await accountService.GetCustomerBalanceAsync(customerId);
        return Results.Ok(result);
    }

    public async Task<IResult> GetTopUps([FromServices] IAccountService accountService, [FromServices] IUser user)
    {
        if (!Guid.TryParse(user.Id, out var customerId))
        {
            return Results.BadRequest("Invalid customerId");
        }
        
        var result = await accountService.GetCustomerToUpsAsync(customerId);
        return Results.Ok(result);
    }

    public async Task<IResult> GetTransactions([FromServices] IAccountService accountService,
        [FromServices] IUser user)
    {
        if (!Guid.TryParse(user.Id, out var customerId))
        {
            return Results.BadRequest("Invalid customerId");
        }
        
        var result = await accountService.GetCustomerTransactionsAsync(customerId);
        return Results.Ok(result);
    }

    public async Task<IResult> GetWithdraws([FromServices] IAccountService accountService,
        [FromServices] IUser user)
    {
        if (!Guid.TryParse(user.Id, out var customerId))
        {
            return Results.BadRequest("Invalid customerId");
        }
        
        var result = await accountService.GetCustomerWithdrawsAsync(customerId);
        return Results.Ok(result);
    }

    public async Task<IResult> DeleteAccount([FromServices] IAccountService accountService,
        [FromServices] IUser user)
    {
        if (!Guid.TryParse(user.Id, out var customerId))
        {
            return Results.BadRequest("Invalid customerId");
        }
        var result =await accountService.DeleteAccountAsync(customerId);
        
        if (result)
        {
            return Results.Ok("Acccount successfully deleted!");
        }
        
        return Results.BadRequest("Account could not be deleted");
    }
}