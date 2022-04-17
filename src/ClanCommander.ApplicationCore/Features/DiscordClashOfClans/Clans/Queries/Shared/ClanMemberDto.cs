namespace ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Clans.Queries.Shared;

public class ClanMemberDto
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int ExpLevel { get; set; }
    public ulong UserId { get; set; }
    public string ClanRole { get; set; } = default!;
}
