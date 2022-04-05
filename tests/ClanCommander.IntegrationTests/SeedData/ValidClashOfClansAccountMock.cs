using ClanCommander.ApplicationCore.Entities.DiscordClashOfClans.Users;

namespace ClanCommander.IntegrationTests.SeedData;

public class ValidClashOfClansAccountMock
{
    internal PlayerId AccountId = PlayerId.FromString("#PQU9QLP2V");
    internal DiscordUserId UserId = DiscordUserId.FromUInt64(339924145909399562u);
    internal string UserName = "JAY";
    private readonly ClashOfClansAccount _accountEntity;
    public ValidClashOfClansAccountMock()
    {
        _accountEntity = new ClashOfClansAccount(AccountId, UserId, UserName);
    }

    internal async Task SeedToDatabaseAsync(IServiceProvider serviceProvider)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.AddAsync(_accountEntity);
        await context.SaveChangesAsync();
    }
}
