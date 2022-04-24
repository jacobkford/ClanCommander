namespace ClanCommander.ApplicationCore.Features.Discord.Guilds.Commands.ChangeDiscordGuildMessageCommandsPrefix;

public class ChangeDiscordGuildMessageCommandsPrefixCommand : IRequest<ChangeDiscordGuildMessageCommandsPrefixDto>
{
    public ulong GuildId { get; set; }
    public string Prefix { get; set; }

    public ChangeDiscordGuildMessageCommandsPrefixCommand(ulong guildId, string prefix)
    {
        GuildId = guildId;
        Prefix = prefix;
    }

    internal class ChangeGuildMessageCommandsPrefixCommandHandler : IRequestHandler<ChangeDiscordGuildMessageCommandsPrefixCommand, ChangeDiscordGuildMessageCommandsPrefixDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICacheService _cacheService;

        public ChangeGuildMessageCommandsPrefixCommandHandler(IServiceProvider serviceProvider, ICacheService cacheService)
        {
            _serviceProvider = serviceProvider;
            _cacheService = cacheService;
        }

        public async Task<ChangeDiscordGuildMessageCommandsPrefixDto> Handle(ChangeDiscordGuildMessageCommandsPrefixCommand request, CancellationToken cancellationToken)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>()
                ?? throw new NullReferenceException();

            var guildMessageCommandsConfig = await dbContext.MessageCommandsConfigurations
                .SingleOrDefaultAsync(x => x.GuildId == DiscordGuildId.FromUInt64(request.GuildId));

            guildMessageCommandsConfig ??= new GuildMessageCommands(DiscordGuildId.FromUInt64(request.GuildId));
            
            var oldPrefix = guildMessageCommandsConfig.MessageCommandPrefix;

            guildMessageCommandsConfig.ChangeMessageCommandPrefix(request.Prefix);
            await dbContext.SaveEntitiesAsync(cancellationToken);

            await _cacheService.SetAsync($"discord:guild:{request.GuildId}:prefix", request.Prefix, TimeSpan.FromDays(30), TimeSpan.FromDays(2));

            return new ChangeDiscordGuildMessageCommandsPrefixDto
            {
                GuildId = request.GuildId,
                OldPrefix = oldPrefix,
                NewPrefix = request.Prefix,
            };
        }
    }
}
