using Discord;

namespace ClanCommander.SharedKernel.DiscordEmbedBuilders;

public class SuccessEmbedBuilder : EmbedBuilder
{
    public SuccessEmbedBuilder() : base()
    {
        Color = Discord.Color.Green;
    }
}
