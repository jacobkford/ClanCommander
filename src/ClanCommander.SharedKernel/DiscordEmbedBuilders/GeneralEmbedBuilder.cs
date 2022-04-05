using Discord;

namespace ClanCommander.SharedKernel.DiscordEmbedBuilders;

public class GeneralEmbedBuilder : EmbedBuilder
{
    public GeneralEmbedBuilder() : base()
    {
        Color = new Discord.Color(3, 28, 252);
    }
}