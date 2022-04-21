namespace ClanCommander.IntegrationTests.Mocks;

public class DiscordUserServiceMock : IDiscordUserService
{
    public async Task<string?> GetUsername(ulong userId)
    {
        return userId == 339924145909399562u ? "TestUser#0001" : null;
    }

    public bool IsBotOwner(ulong userId)
    {
        return userId == 339924145909399562u;
    }

    public async Task<bool> IsGuildOwner(ulong guildId, ulong userId)
    {
        return userId == 339924145909399562u;
    }
}
