namespace ClanCommander.ApplicationCore;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("PostgreSQL"));
        });

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = "ClanCommander_";
        });

        services.AddClashOfClans(options =>
        {
            options.Tokens.Add(configuration["ClashOfClans:Token"]);
        });
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient<ApplicationDbContextSeeder>();
        services.AddTransient<ICacheService, CacheService>();
        services.AddTransient<IMessageCommandService, MessageCommandService>();

        return services;
    }

    public static async Task SeedDatabaseAsync(this IHost app)
    {
        var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

        using var scope = scopedFactory.CreateScope();

        var service = scope.ServiceProvider.GetService<ApplicationDbContextSeeder>();

        await service.SeedAsync();
    }
}
