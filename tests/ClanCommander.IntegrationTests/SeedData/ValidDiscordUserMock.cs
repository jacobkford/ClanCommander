namespace ClanCommander.IntegrationTests.SeedData;

public class ValidDiscordUserMock
{
    internal DiscordUserId UserId = DiscordUserId.FromUInt64(339924145909399562u);
    internal string UserName = "TestUser#0001";
    private readonly DiscordUser _userEntity;

    public ValidDiscordUserMock()
    {
        _userEntity = new DiscordUser(UserId, UserName);
    }

    internal async Task SeedToDatabaseAsync(IServiceProvider serviceProvider)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.AddAsync(_userEntity);
        await context.SaveChangesAsync();
    }
}
