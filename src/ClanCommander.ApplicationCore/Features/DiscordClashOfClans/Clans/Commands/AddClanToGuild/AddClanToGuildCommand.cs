namespace ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Clans.Commands.AddClanToGuild;

public class AddClanToGuildCommand : IRequest<AddClanToGuildDto>
{
    public ulong GuildId { get; set; }

    public string ClanId { get; set; }

    public AddClanToGuildCommand(ulong guildId, string clanId)
    {
        GuildId = guildId;
        ClanId = clanId.ToUpper();
    }

    internal class AddClanToGuildCommandHandler : IRequestHandler<AddClanToGuildCommand, AddClanToGuildDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IClashOfClansApiClanService _clanApiService;

        public AddClanToGuildCommandHandler(IServiceProvider serviceProvider, IClashOfClansApiClanService clanApiService)
        {
            _serviceProvider = serviceProvider;
            _clanApiService = clanApiService;
        }

        public async Task<AddClanToGuildDto> Handle(AddClanToGuildCommand request, CancellationToken cancellationToken)
        {
            var requestClanId = Entities.ClashOfClans.ClanId.FromString(request.ClanId);
            var requestGuildId = DiscordGuildId.FromUInt64(request.GuildId);

            await using var scope = _serviceProvider.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>()
                ?? throw new NullReferenceException();

            var clanData = await _clanApiService.GetClanAsync(request.ClanId)
                ?? throw new ArgumentException($"Clan with the Id of '{request.ClanId}' was not found.");

            var guild = await dbContext.DiscordGuilds.FirstOrDefaultAsync(x => x.GuildId == requestGuildId, cancellationToken)
                ?? throw new ArgumentException($"There was an issue registering your discord server. Remove this bot from the server & invite it back.");
            
            var clanAlreadyRegistered = await dbContext.GuildClans.AnyAsync(
                guildClan => guildClan.ClanId == requestClanId && guildClan.GuildId == requestGuildId, cancellationToken);

            if (clanAlreadyRegistered)
                throw new ArgumentException($"{clanData.Name} has already been registered in this server.");
            

            var clan = guild.CreateClashOfClansClan(requestClanId, clanData.Name);
            await dbContext.AddAsync(clan, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new AddClanToGuildDto { GuildId = request.GuildId, ClanId = request.ClanId };
        }
    }
}
