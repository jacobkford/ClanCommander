namespace ClanCommander.ApplicationCore.Features.Misc;

public class MiscCommands : ModuleBase<SocketCommandContext>
{
    [Command("ping")]
    public async Task PingAsync()
        => await Context.Channel.SendBasicEmbedAsync($"**Pong!** *({Context.Client.Latency}ms)*");
}
