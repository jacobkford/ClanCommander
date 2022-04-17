using ClanCommander.ApplicationCore.Data.ValueConverters;
using Dapper;

namespace ClanCommander.IntegrationTests;

public abstract class TestBase : IDisposable
{
    protected readonly IConfigurationRoot Configuration;
    protected readonly IServiceProvider ServiceProvider;
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

                SqlMapper.AddTypeHandler(new ClanIdTypeHandler());
                SqlMapper.AddTypeHandler(new DiscordGuildIdTypeHandler());
                SqlMapper.AddTypeHandler(new DiscordUserIdTypeHandler());
                SqlMapper.AddTypeHandler(new PlayerIdTypeHandler());
                SqlMapper.AddTypeHandler(new ClanMemberRoleTypeHandler());

                services.AddTransient<ICacheService, RedisCacheService>();
                services.AddTransient<IMessageCommandService, MessageCommandService>();
                services.AddTransient<IClashOfClansApiClanService, ClashOfClansApiClanServiceMock>();
                services.AddTransient<IClashOfClansApiPlayerService, ClashOfClansApiPlayerServiceMock>();
            }).Build();

        ServiceProvider = host.Services;
        Mediator = host.Services.GetRequiredService<IMediator>();
        RedisCache = host.Services.GetRequiredService<IDistributedCache>();

        var dbContext = host.Services.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.Migrate();
        dbContext.Database.EnsureCreated();
    }

    public async Task Setup()
    {
        await using var scope = ServiceProvider.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.Database.MigrateAsync();
        await context.Database.EnsureCreatedAsync();
    }

    public void Dispose()
    {
        using var scope = ServiceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Database.EnsureDeleted();
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