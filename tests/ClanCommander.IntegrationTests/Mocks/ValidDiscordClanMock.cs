namespace ClanCommander.IntegrationTests.Mocks;

public class ValidDiscordClanMock
{
    internal ClanId ClanId = ClanId.FromString("#9UGQ0GL");
    internal string ClanName = "PlaneClashers";
    internal DiscordGuildId GuildId = DiscordGuildId.FromUInt64(760910445686161488u);
    internal ulong ClanIdentifierRoleId = 762797662013227040u;

    internal PlayerId LeaderId = PlayerId.FromString("#PQU9QLP2V");
    internal DiscordUserId LeaderUserId = DiscordUserId.FromUInt64(339924145909399562u);
    internal ClanMemberRole LeaderRole = ClanMemberRole.Leader;

    private readonly GuildClan _clanEntity;

    public ValidDiscordClanMock()
    {
        _clanEntity = new GuildClan(ClanId, ClanName, GuildId);
        _clanEntity.ChangeDiscordRole(ClanIdentifierRoleId);
    }

    internal void WithValidClanMember()
    {
        _clanEntity.AddClanMember(LeaderId, LeaderUserId);
    }

    internal async Task SeedToDatabaseAsync(IServiceProvider serviceProvider)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.AddAsync(_clanEntity);
        await context.SaveChangesAsync();
    }
}
