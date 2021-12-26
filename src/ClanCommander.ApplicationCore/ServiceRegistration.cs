using ClanCommander.ApplicationCore.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClanCommander.ApplicationCore;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationCore(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddScoped<MailService>();
        services.AddClashOfClans(options =>
        {
            options.Tokens.Add(configuration["ClashOfClans:Token"]);
        });

        services.AddScoped<IMailService, MailService>();
        services.AddScoped<IClashOfClansService, ClashOfClansService>();

        return services;
    }
}

