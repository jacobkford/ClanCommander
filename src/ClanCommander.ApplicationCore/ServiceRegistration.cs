namespace ClanCommander.ApplicationCore;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationContext>(options 
            => options.UseSqlite(configuration.GetConnectionString("ClanCommanderSqlite")));

        services.AddClashOfClans(options =>
        {
            options.Tokens.Add(configuration["ClashOfClans:Token"]);
        });

        // services.AddScoped<IMailService, MailService>();
        services.AddScoped<IClashOfClansService, ClashOfClansService>();

        return services;
    }
}

