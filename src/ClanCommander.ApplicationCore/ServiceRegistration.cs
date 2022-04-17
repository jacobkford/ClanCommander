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

        SqlMapper.AddTypeHandler(new ClanIdTypeHandler());
        SqlMapper.AddTypeHandler(new DiscordGuildIdTypeHandler());
        SqlMapper.AddTypeHandler(new DiscordUserIdTypeHandler());
        SqlMapper.AddTypeHandler(new PlayerIdTypeHandler());
        SqlMapper.AddTypeHandler(new ClanMemberRoleTypeHandler());

        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient<ApplicationDbContextSeeder>();
        services.AddTransient<ICacheService, RedisCacheService>();
        services.AddTransient<IMessageCommandService, MessageCommandService>();
        services.AddTransient<IClashOfClansApiClanService, ClashOfClansApiClanService>();
        services.AddTransient<IClashOfClansApiPlayerService, ClashOfClansApiPlayerService>();

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
