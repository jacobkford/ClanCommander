namespace ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Clans.Commands.RemoveGuildClanMember;

public class RemoveGuildClanMemberCommand : IRequest
{
    public ulong GuildId { get; private set; }
    public string ClanId { get; private set; }
    public string MemberId { get; private set; }

    public RemoveGuildClanMemberCommand(ulong guildId, string clanId, string memberId)
    {
        GuildId = guildId;
        ClanId = clanId.ToUpper();
        MemberId = memberId.ToUpper();
    }

    internal class RemoveGuildClanMemberCommandHandler : IRequestHandler<RemoveGuildClanMemberCommand>
    {
        private readonly IServiceProvider _serviceProvider;

        public RemoveGuildClanMemberCommandHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<Unit> Handle(RemoveGuildClanMemberCommand request, CancellationToken cancellationToken)
        {
            var requestClanId = Entities.ClashOfClans.ClanId.FromString(request.ClanId);
            var requestGuildId = DiscordGuildId.FromUInt64(request.GuildId);
            var requestMemberId = PlayerId.FromString(request.MemberId);

            await using var scope = _serviceProvider.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>()
                ?? throw new NullReferenceException();

            var clan = await dbContext.GuildClans.FirstOrDefaultAsync(guildClan => guildClan.ClanId == requestClanId && guildClan.GuildId == requestGuildId, cancellationToken)
                ?? throw new ArgumentException($"Clan with the Id of '{request.ClanId}' was not found.");

            clan.RemoveClanMember(requestMemberId);
            await dbContext.SaveEntitiesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
