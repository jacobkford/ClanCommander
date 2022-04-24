namespace ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Clans.Commands.RemoveGuildClan;

public class RemoveClanFromGuildCommand : IRequest
{
    public ulong GuildId { get; set; }

    public string ClanId { get; set; }

    public RemoveClanFromGuildCommand(ulong guildId, string clanId)
    {
        GuildId = guildId;
        ClanId = clanId;
    }

    internal class RemoveClanFromGuildCommandHandler : IRequestHandler<RemoveClanFromGuildCommand>
    {
        private readonly IServiceProvider _serviceProvider;

        public RemoveClanFromGuildCommandHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<Unit> Handle(RemoveClanFromGuildCommand request, CancellationToken cancellationToken)
        {
            var requestClanId = Entities.ClashOfClans.ClanId.FromString(request.ClanId);
            var requestGuildId = DiscordGuildId.FromUInt64(request.GuildId);

            await using var scope = _serviceProvider.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>()
                ?? throw new NullReferenceException();

            var clan = await dbContext.GuildClans.FirstOrDefaultAsync(guildClan => guildClan.ClanId == requestClanId && guildClan.GuildId == requestGuildId, cancellationToken);
            
            if (clan is not null)
            {
                dbContext.Remove(clan);
                await dbContext.SaveEntitiesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
