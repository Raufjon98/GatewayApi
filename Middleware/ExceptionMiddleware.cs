using System.Text.Json;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (RpcException e)
        {
            var problemDetails = new ProblemDetails();
            if (e.StatusCode == StatusCode.NotFound)
            {
                problemDetails.Status = StatusCodes.Status404NotFound;
                problemDetails.Title = "Object Not Found";
                problemDetails.Detail = e.Message;
            }else if (e.StatusCode == StatusCode.AlreadyExists)
            {
                problemDetails.Status = StatusCodes.Status409Conflict;
                problemDetails.Title = "Object Already Exists";
                problemDetails.Detail = e.Message;
            }
            else if (e.StatusCode == StatusCode.InvalidArgument)
            {
                foreach (var entry in e.Trailers)
                {
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = "Validation Failed";
                    problemDetails.Detail = entry.Value;
                }
            }
            else
            {
                problemDetails.Status = StatusCodes.Status500InternalServerError;
                problemDetails.Title = "Internal Server Error";
                problemDetails.Detail = e.Message;
            }
            context.Response.StatusCode = problemDetails.Status!.Value;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
        }
    }
}