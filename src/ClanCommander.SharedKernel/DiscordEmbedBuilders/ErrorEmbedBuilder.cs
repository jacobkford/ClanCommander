using Discord;

namespace ClanCommander.SharedKernel.DiscordEmbedBuilders;

public class ErrorEmbedBuilder : EmbedBuilder
{
    public ErrorEmbedBuilder() : base()
    {
        Color = Discord.Color.Red;
    }
}
