using ApiGateway.Consumers.Accounts;
using ApiGateway.Extensions;
using ApiGateway.Interfaces;
using ApiGateway.Middleware;
using ApiGateway.Services;
using CatalogService.Contracts.Extensions;
using CustomerService.Contracts.Extensions;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OrderService.Contracts.Extensions;
using PaymentService.Contracts.Extentions;
using RabbitMQ.Client;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Components ??= new();
        document.Components.SecuritySchemes ??= new Dictionary<string, OpenApiSecurityScheme>();
        
        document.Components.SecuritySchemes.Add("Bearer", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description = "Enter your JWT token in the format: Bearer {your token}"
        });

        document.SecurityRequirements = new List<OpenApiSecurityRequirement>
        {
            new()
            {
                [new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                }] = Array.Empty<string>()
            }
        };

        return Task.CompletedTask;
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
        options.DefaultChallengeScheme =
            options.DefaultForbidScheme =
                options.DefaultScheme =
                    options.DefaultSignInScheme =
                        options.DefaultSignOutScheme =
                            JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SignInKey"]!)),
    };
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddCustomerServiceContracts();
builder.Services.AddCatalogServiceContracts();
builder.Services.AddPaymentServiceContracts();
builder.Services.AddOrderServiceContracts();
builder.Services.AddAuthorization();
builder.Services.AddScoped<IUser, CurrentUser>();

var rabbitConnectionString = builder.Configuration["MessageBroker:Host"];

builder.Services.AddMassTransit(configuration =>
{
    configuration.AddConsumer<AccountCreatedConsumer>();
    configuration.AddConsumer<AccountDeletedConsumer>();
    configuration.AddConsumer<AccountUpdatedConsumer>();
   
    configuration.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(rabbitConnectionString);
        cfg.ExchangeType = ExchangeType.Fanout;
        cfg.ConfigureEndpoints(ctx);
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
else
{
    app.UseExceptionHandler(); // Handles unhandled exceptions
    app.UseStatusCodePages(); // Ensures body for non-success status codes
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.MapEndpoints();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.Run();


