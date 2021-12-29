namespace ClanCommander.ApplicationCore.Extensions;

public static class DiscordEmbedExtensions
{
    public static async Task<IMessage> SendBasicEmbedAsync(
        this ISocketMessageChannel channel,
        string message,
        SocketUser? user = null,
        Color? color = null)
    {
        var embed = new EmbedBuilder()
            .WithDescription(message);

        if (color != null)
            embed.Color = color;

        if (user != null)
            return await channel.SendMessageAsync(text: $"<@{user.Id}", embed: embed.Build());
        else
            return await channel.SendMessageAsync(embed: embed.Build());
    }
}
