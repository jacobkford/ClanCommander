namespace ClanCommander.DiscordBot.Modules;

public class MiscModule : ModuleBase<SocketCommandContext>
{
    [Command("ping")]
    public async Task PingAsync()
        => await Context.Channel.SendBasicEmbedAsync($"**Pong!** *({Context.Client.Latency}ms)*");
}
