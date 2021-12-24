﻿using Discord.Commands;

namespace ClanCommander.Application.Misc;
public class MiscCommands : ModuleBase<SocketCommandContext>
{
    [Command("ping")]
    public async Task PingAsync()
        => await Context.Channel.SendMessageAsync($"**Pong!** *({Context.Client.Latency}ms)*");
}
