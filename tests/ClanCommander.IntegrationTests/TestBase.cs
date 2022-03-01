using ClanCommander.ApplicationCore.Interfaces;
using ClanCommander.ApplicationCore.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Reflection;

namespace ClanCommander.IntegrationTests;

public abstract class TestBase : IDisposable
{
    internal readonly ApplicationDbContext ApplicationDbContext;
    protected readonly IConfigurationRoot Configuration;
    protected readonly IMediator Mediator;
    protected readonly IDistributedCache RedisCache;

    protected TestBase()
    {
        Configuration = this.SetupConfigurationRoot();

        var host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(builder
                => builder.AddConfiguration(Configuration))
            .ConfigureServices(services =>
            {
                services.AddLogging();
                services.AddEntityFrameworkNpgsql();
                services.AddMediatR(Assembly.Load("ClanCommander.ApplicationCore"));
                services.BuildServiceProvider();
                services.AddDbContext<ApplicationDbContext>(
                    options => options.UseNpgsql(Configuration.GetConnectionString("PostgreSQL")));

                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = Configuration.GetConnectionString("Redis");
                    options.InstanceName = $"{GetType().Name}:{Guid.NewGuid()}_";
                });
                services.AddTransient<ICacheService, CacheService>();
                services.AddTransient<IMessageCommandService, MessageCommandService>();
            }).Build();

        ApplicationDbContext = host.Services.GetRequiredService<ApplicationDbContext>();
        Mediator = host.Services.GetRequiredService<IMediator>();
        RedisCache = host.Services.GetRequiredService<IDistributedCache>();

        ApplicationDbContext.Database.Migrate();
        ApplicationDbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        ApplicationDbContext.Database.EnsureDeleted();
    }

    private IConfigurationRoot SetupConfigurationRoot()
    {
        var databaseSuffix = $"{GetType().Name}_{Guid.NewGuid()}";
        var connectionString = $"Host=127.0.0.1;Port=5432;Database={databaseSuffix};Username=postgres;Password=myPassword;";

        var configData = new Dictionary<string, string>
        {
            { "ConnectionStrings:PostgreSQL", connectionString },
            { "ConnectionStrings:Redis", "127.0.0.1:6379" }
        };

        return new ConfigurationBuilder()
            .AddInMemoryCollection(configData)
            .Build();
    }
}