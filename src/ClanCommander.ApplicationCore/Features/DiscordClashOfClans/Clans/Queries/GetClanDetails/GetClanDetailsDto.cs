namespace ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Clans.Queries.GetClanDetails;

public class GetClanDetailsDto
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public bool Registered { get; set; }
    public ulong DiscordGuildId { get; set; }
    public ulong DiscordRoleId { get; set; }
    public int MemberCount { get; set; }
    public int RegisteredMemberCount { get; set; }
    public string? LeaderName { get; set; }
    public ulong LeaderDiscordUserId { get; set; }
    public string? ClanBadgeUrl { get; set; }
}
