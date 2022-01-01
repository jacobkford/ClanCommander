namespace ClanCommander.ApplicationCore.Entities;

internal class EntryApplication : Entity, IAggregateRoot
{
    public int Id { get; set; }

    public EntryApplicationState State { get; private set; }

    public ulong DiscordServerId { get; private set; }

    public ulong DiscordChannelId { get; private set; }

    public ulong DiscordUserId { get; private set; }

    public string? AccountId { get; private set; }

    public bool AccountPassedValidationChecks { get; private set; }

    public string? ResolvedReason { get; private set; }

    public ulong? ResolvedBy { get; private set; }

    public string? AdditionalResolvedReason { get; private set; }

    public EntryApplication(
        ulong discordServerId,
        ulong discordChannelId,
        ulong discordUserId)
    {
        DiscordServerId = Guard.Against.InvalidDiscordSnowflakeId(discordServerId, nameof(discordServerId));
        DiscordChannelId = Guard.Against.InvalidDiscordSnowflakeId(discordChannelId, nameof(discordChannelId));
        DiscordUserId = Guard.Against.InvalidDiscordSnowflakeId(discordUserId, nameof(discordUserId));
        AccountId = null;
        State = EntryApplicationState.Active;
    }

    public void AddAccount(string accountId)
    {
        AccountId = Guard.Against.InvalidClashOfClansTag(
            accountId, 
            nameof(accountId),
            "Account Id must be a valid playertag or cannot be empty.");
    }

    public void ResolveAsSuccess(string resolvedReason, ulong resolvedBy)
    {
        if (!AccountPassedValidationChecks)
            throw new ArgumentException();

        State = EntryApplicationState.Success;
        ResolvedReason = Guard.Against.NullOrWhiteSpace(resolvedReason, nameof(resolvedReason));
        ResolvedBy = Guard.Against.InvalidDiscordSnowflakeId(resolvedBy, nameof(resolvedBy));
    }

    public void ResolvedAsFailure(string resolvedReason, ulong resolvedBy)
    {
        State = EntryApplicationState.Failure;
        ResolvedReason = Guard.Against.NullOrWhiteSpace(resolvedReason, nameof(resolvedReason));
        ResolvedBy = Guard.Against.InvalidDiscordSnowflakeId(resolvedBy, nameof(resolvedBy));
    }

    public void AddAdditionalResolvedReason(string reason)
    {
        AdditionalResolvedReason = Guard.Against.NullOrWhiteSpace(reason, nameof(reason));
    }
}
