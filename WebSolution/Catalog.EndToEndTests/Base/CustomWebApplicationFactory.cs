using Catalog.Data.Configurations;
using Catalog.Infra.Messaging.Configuration;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;

namespace Catalog.EndToEndTests.Base;

public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup>, IDisposable
    where TStartup : class
{
    private const string VideoCreatedRoutingKey = "video.created";
    public string VideoEncodedRoutingKey => "video.encoded";
    public string VideoCreatedQueue => "video.created.queue";
    public Mock<StorageClient> StorageClient { get; private set; }
    //public IModel RabbitMQChannel { get; private set; }
    public RabbitMQConfiguration RabbitMQConfiguration { get; private set; }
    protected override void ConfigureWebHost(
        IWebHostBuilder builder
    )
    {
        builder.UseEnvironment("Catalog.EndToEndTests");
        builder.ConfigureServices(services =>
        {
            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
                var context = scope.ServiceProvider
                .GetService<CatalogDbContext>();            
                ArgumentNullException.ThrowIfNull(nameof(context));
            context.Database.EnsureDeleted();
            context.Database.EnsureDeleted();

        });

        base.ConfigureWebHost(builder);
    }
    /*
    public void SetupRabbitMQ()
    {
        var channel = RabbitMQChannel!;
        var exchange = RabbitMQConfiguration!.Exchange;
        channel.ExchangeDeclare(exchange, "direct", true, false, null);
        channel.QueueDeclare(VideoCreatedQueue, true, false, false, null);
        channel.QueueBind(VideoCreatedQueue, exchange, VideoCreatedRoutingKey, null);
        channel.QueueDeclare(RabbitMQConfiguration.VideoEncodedQueue,
            true, false, false, null);
        channel.QueueBind(RabbitMQConfiguration.VideoEncodedQueue,
            exchange, VideoEncodedRoutingKey, null);
    }

    private void TearDownRabbitMQ()
    {
        var channel = RabbitMQChannel!;
        var exchange = RabbitMQConfiguration!.Exchange;
        channel.QueueUnbind(VideoCreatedQueue, exchange, VideoCreatedRoutingKey, null);
        channel.QueueDelete(VideoCreatedQueue, false, false);
        channel.QueueUnbind(RabbitMQConfiguration.VideoEncodedQueue,
            exchange, VideoEncodedRoutingKey, null);
        channel.QueueDelete(RabbitMQConfiguration.VideoEncodedQueue, false, false);
        channel.ExchangeDelete(exchange, false);
    }
    */
    public override ValueTask DisposeAsync()
    {
        //TearDownRabbitMQ();
        return base.DisposeAsync();
    }
}