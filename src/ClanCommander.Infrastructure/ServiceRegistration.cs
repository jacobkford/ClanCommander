using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClanCommander.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddScoped<MailService>();
        services.AddClashOfClans(options =>
        {
            options.Tokens.Add(configuration["ClashOfClans:Token"]);
        });

        return services;
    }
}

