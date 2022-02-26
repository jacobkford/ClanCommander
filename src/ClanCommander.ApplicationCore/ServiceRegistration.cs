using Microsoft.Extensions.Hosting;

namespace ClanCommander.ApplicationCore;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options 
            => options.UseNpgsql(configuration.GetConnectionString("PostgreSQL")));

        services.AddClashOfClans(options =>
        {
            options.Tokens.Add(configuration["ClashOfClans:Token"]);
        });

        return services;
    }
}
