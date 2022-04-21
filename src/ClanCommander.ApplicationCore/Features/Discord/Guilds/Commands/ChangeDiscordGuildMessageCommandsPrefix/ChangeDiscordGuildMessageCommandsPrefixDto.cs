namespace ClanCommander.ApplicationCore.Features.Discord.Guilds.Commands.ChangeDiscordGuildMessageCommandsPrefix;

public class ChangeDiscordGuildMessageCommandsPrefixDto
{
    public ulong GuildId { get; set; }
    public string OldPrefix { get; set; } = default!;
    public string NewPrefix { get; set; } = default!;
}
