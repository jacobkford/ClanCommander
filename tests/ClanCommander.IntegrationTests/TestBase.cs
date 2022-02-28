namespace ClanCommander.IntegrationTests;

public abstract class TestBase : IDisposable
{
    internal readonly ApplicationDbContext ApplicationDbContext;
    protected readonly IConfigurationRoot Configuration;
    private readonly IServiceScopeFactory ScopeFactory;
    protected readonly IMediator Mediator;

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
                services.AddMediatR(typeof(GetDiscordGuildDetailsQuery).Assembly);
                services.BuildServiceProvider();
                services.AddDbContext<ApplicationDbContext>(
                    options => options.UseNpgsql(Configuration.GetConnectionString("PostgreSQL")));
            }).Build();

        ScopeFactory = host.Services.GetRequiredService<IServiceScopeFactory>();
        ApplicationDbContext = host.Services.GetRequiredService<ApplicationDbContext>();
        Mediator = host.Services.GetRequiredService<IMediator>();

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
        };

        return new ConfigurationBuilder()
            .AddInMemoryCollection(configData)
            .Build();
    }
}