namespace ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Clans.Queries.GetClanRoster;

public class GetClanMembersDto
{
    public string ClanId { get; set; } = default!;

    public string ClanName { get; set; } = default!;

    public string ClanBadgeUrl { get; set; } = default!;

    public ulong DiscordGuildId { get; set; }

    public IReadOnlyCollection<ClanMemberDto>? Members { get; set; }
}
