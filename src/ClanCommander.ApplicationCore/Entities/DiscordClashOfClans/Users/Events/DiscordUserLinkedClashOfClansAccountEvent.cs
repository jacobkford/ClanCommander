namespace ClanCommander.ApplicationCore.Entities.DiscordClashOfClans.Users.Events;

internal class DiscordUserLinkedClashOfClansAccountEvent : DomainEvent
{
    public PlayerId AccountId { get; private set; }

    public DiscordUserId UserId { get; private set; }

    public string AccountName { get; private set; }

    public DiscordUserLinkedClashOfClansAccountEvent(PlayerId accountId, DiscordUserId userId, string accountName)
    {
        AccountId = accountId;
        UserId = userId;
        AccountName = accountName;
    }
}
