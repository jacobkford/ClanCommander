namespace ClanCommander.IntegrationTests.Mocks;

public class ValidDiscordClanMock
{
    internal ClanId ClanId = ClanId.FromString("#9UGQ0GL");
    internal string ClanName = "PlaneClashers";
    internal DiscordGuildId GuildId = DiscordGuildId.FromUInt64(760910445686161488u);

    internal PlayerId LeaderId = PlayerId.FromString("#PQU9QLP2V");
    internal DiscordUserId LeaderUserId = DiscordUserId.FromUInt64(339924145909399562u);
    internal ClanMemberRole LeaderRole = ClanMemberRole.Leader;

    private readonly GuildClan _clanEntity;

    public ValidDiscordClanMock()
    {
        _clanEntity = new GuildClan(ClanId, ClanName, GuildId);
        _clanEntity.AddClanMember(LeaderId, LeaderUserId, LeaderRole);
    }

    internal async Task SeedToDatabaseAsync(IServiceProvider serviceProvider)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.AddAsync(_clanEntity);
        await context.SaveChangesAsync();
    }
}
