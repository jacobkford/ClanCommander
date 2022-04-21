namespace ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Clans.Commands.AddClanToGuild;

public class AddClanToGuildDto
{
    public ulong GuildId { get; set; }
    public string ClanId { get; set; } = default!;
}
