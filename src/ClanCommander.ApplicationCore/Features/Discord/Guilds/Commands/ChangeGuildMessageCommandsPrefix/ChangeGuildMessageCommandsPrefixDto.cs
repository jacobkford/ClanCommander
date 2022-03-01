namespace ClanCommander.ApplicationCore.Features.Discord.Guilds.Commands.ChangeGuildMessageCommandsPrefix;

public class ChangeGuildMessageCommandsPrefixDto
{
    public ulong GuildId { get; set; }
    public string OldPrefix { get; set; } = default!;
    public string NewPrefix { get; set; } = default!;
}
