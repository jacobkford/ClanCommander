namespace ClanCommander.ApplicationCore.Features.Discord.Users.ClientEvents;

public class UserLeftGuildClientEvent : INotification
{
    public ulong UserId { get; set; }
    public ulong GuildId { get; set; }

    public UserLeftGuildClientEvent(ulong userId, ulong guildId)
    {
        UserId = userId;
        GuildId = guildId;
    }

    internal class UserLeftGuildClientEventHandler : INotificationHandler<UserLeftGuildClientEvent>
    {
        public Task Handle(UserLeftGuildClientEvent notification, CancellationToken cancellationToken)
        {
            // TODO
            return Task.CompletedTask;
        }
    }
}
