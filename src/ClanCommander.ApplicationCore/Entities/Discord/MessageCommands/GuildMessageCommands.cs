﻿namespace ClanCommander.ApplicationCore.Entities.MessageCommands;

internal class GuildMessageCommands : Entity, IAggregateRoot
{
    public DiscordGuildId GuildId { get; private set; }

    public string MessageCommandPrefix { get; private set; }


#pragma warning disable CS8618
    // For EF Core
    private GuildMessageCommands() { }
#pragma warning restore CS8618

    public GuildMessageCommands(DiscordGuildId guildId)
    {
        Guard.Against.InvalidDiscordSnowflakeId(guildId.Value, nameof(guildId));

        GuildId = guildId;
        MessageCommandPrefix = "!";
    }

    public void ChangeMessageCommandPrefix(string newPrefix)
    {
        Guard.Against.NullOrWhiteSpace(newPrefix, nameof(newPrefix), "Prefix was not provided");

        if (newPrefix.Length > 2)
        {
            throw new ArgumentOutOfRangeException(nameof(newPrefix), "Message command prefix's cannot be longer than 2 characters");
        }

        MessageCommandPrefix = newPrefix;
    }
}
