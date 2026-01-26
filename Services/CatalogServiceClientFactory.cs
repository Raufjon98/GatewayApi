using Grpc.Net.Client;
using MagicOnion.Client;

namespace ApiGateway.Services;

public class CatalogServiceClientFactory
{
    private readonly GrpcChannel _channel;

    public CatalogServiceClientFactory(IConfiguration configuration)
    {
        var catalogService = configuration["CatalogService:Url"]
            ?? "https://localhost:5001";
        _channel = GrpcChannel.ForAddress(catalogService, new GrpcChannelOptions
        {
            HttpHandler = new SocketsHttpHandler
            {
                PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
                KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
                EnableMultipleHttp2Connections = true
            }
        });
        // public IAddressService CreateAddressClient()
        // {
        //     return MagicOnionClient.Create<IAddressService>(_channel);
        // }
    }
}