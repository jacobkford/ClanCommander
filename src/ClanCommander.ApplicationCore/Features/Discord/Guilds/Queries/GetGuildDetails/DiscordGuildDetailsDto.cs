namespace ClanCommander.ApplicationCore.Features.Discord.Guilds.Queries.GetGuildDetails;

public class DiscordGuildDetailsDto
{
    public ulong Id { get; set; }
    public string Name { get; set; } = default!;
    public ulong OwnerId { get; set; }
    public string? MessageCommandPrefix { get; set; }
}
