using System.Text.Json;
using ApiGateway.Extensions;
using CatalogService.Contracts.Address.Requests;
using CatalogService.Contracts.Address.Resposes;
using CatalogService.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace ApiGateway.Endpoints;

public class Addresses : EndpointGroupBase
{
    public override string Prefix  => "addresses";
     public override void Map(WebApplication app)
     {
         var group = app.MapGroup(Prefix);
         group.MapGet("/", GetAll);
         group.MapGet("/{addressId}", GetById);
         group.MapPost("/", Create);
         group.MapPut("/{addressId}", Update);
         group.MapPost("/delete", Delete);
     }
     public async Task<IResult> GetAll([FromServices] IAddressService addressService)
     {
         var result = await addressService.GetAllAddressesAsync();
         return Results.Ok(result);
     }

     public async Task<IResult> GetById(
         [FromServices] IAddressService addressService,
         [FromServices] IDistributedCache cache,
         [FromQuery] string addressId)
     {
         var key = $"address:{addressId}";
         var cachced = await cache.GetStringAsync(key);
         if (cachced != null)
         {
             return Results.Ok(JsonSerializer.Deserialize<AddressResponse>(cachced));
         }
         
         var result = await addressService.GetAddressAsync(addressId);
         await cache.SetStringAsync(key,
             JsonSerializer.Serialize(result),
             new DistributedCacheEntryOptions
             {
                 AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
             });
         return Results.Ok(result);
     }

     public async Task<IResult> Create([FromServices] IAddressService addressService, [FromBody] CreateAddressRequest address)
     {
         var result = await addressService.CreateAddressAsync(address);
         return Results.Ok(result);
     }

     public async Task<IResult> Update([FromServices] IAddressService addressService, [FromQuery]  string addressId, [FromBody] CreateAddressRequest address)
     {
         var result = await addressService.UpdateAddressAsync(addressId, address);
         return Results.Ok(result);
     }

     public async Task<IResult> Delete([FromServices] IAddressService addressService, [FromQuery] string addressId)
     {
         var result = await addressService.DeleteAddressAsync(addressId);
         return Results.Ok(result);
     }
}