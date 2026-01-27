using CatalogService.MagicOnion.Interfaces;
using CustomerApi.MagicOnion.Interfaces;
using Grpc.Net.Client;
using MagicOnion.Client;

namespace ApiGateway.Extensions;

public static class ClientExtensions
{
    public static IServiceCollection AddClients(this IServiceCollection services, IConfiguration configuration)
    {
        var catalogServiceUrl = configuration["Services:CatalogService:Url"] 
            ?? "https://localhost:5001";
        
        services.AddSingleton(provider =>
        {
            var channel = GrpcChannel.ForAddress(catalogServiceUrl, new GrpcChannelOptions
            {
                HttpHandler = CreateHttpHandler()
            });
            return channel;
        });
        
        var customerServiceUrl = configuration["Services:CustomerService:Url"] 
                                ?? "https://localhost:5051";
        
        services.AddSingleton(provider =>
        {
            var channel = GrpcChannel.ForAddress(customerServiceUrl, new GrpcChannelOptions
            {
                HttpHandler = CreateHttpHandler()
            });
            return channel;
        });
        
        services.AddSingleton<IAuthService>(provider =>
        {
            var channel = provider.GetRequiredService<GrpcChannel>();
            return MagicOnionClient.Create<IAuthService>(channel);
        });

        services.AddSingleton<IUserService>(provider =>
        {
            var channel = provider.GetRequiredService<GrpcChannel>();
            return MagicOnionClient.Create<IUserService>(channel);
        });

        services.AddSingleton<IAddressService>(provider =>
        {
            var channel = provider.GetRequiredService<GrpcChannel>();
            return MagicOnionClient.Create<IAddressService>(channel);
        });
        
        services.AddSingleton<ICategoryService>(provider =>
        {
            var channel = provider.GetRequiredService<GrpcChannel>();
            return MagicOnionClient.Create<ICategoryService>(channel);
        });
        
        services.AddSingleton<IFoodCategoryService>(provider =>
        {
            var channel = provider.GetRequiredService<GrpcChannel>();
            return MagicOnionClient.Create<IFoodCategoryService>(channel);
        });
        
        services.AddSingleton<ICuisineService>(provider =>
        {
            var channel = provider.GetRequiredService<GrpcChannel>();
            return MagicOnionClient.Create<ICuisineService>(channel);
        });
        
        services.AddSingleton<IFoodService>(provider =>
        {
            var channel = provider.GetRequiredService<GrpcChannel>();
            return MagicOnionClient.Create<IFoodService>(channel);
        });
        
        services.AddSingleton<IRestaurantService>(provider =>
        {
            var channel = provider.GetRequiredService<GrpcChannel>();
            return MagicOnionClient.Create<IRestaurantService>(channel);
        });
        
        return services;
    }
    
    private static SocketsHttpHandler CreateHttpHandler()
    {
        return new SocketsHttpHandler
        {
            PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
            KeepAlivePingDelay = TimeSpan.FromSeconds(60),
            KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
            EnableMultipleHttp2Connections = true
        };
    }
}