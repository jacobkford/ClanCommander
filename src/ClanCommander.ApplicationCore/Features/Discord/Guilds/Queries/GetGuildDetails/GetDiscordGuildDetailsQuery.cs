namespace ClanCommander.ApplicationCore.Features.Discord.Guilds.Queries.GetGuildDetails;

public class GetDiscordGuildDetailsQuery : IRequest<DiscordGuildDetailsDto>
{
    public ulong GuildId { get; set; }

    public GetDiscordGuildDetailsQuery(ulong guildId)
    {
        GuildId = guildId;
    }

    public class GetDiscordGuildDetailsQueryHandler : IRequestHandler<GetDiscordGuildDetailsQuery, DiscordGuildDetailsDto?>
    {
        private readonly IDbConnection _db;

        public GetDiscordGuildDetailsQueryHandler(IConfiguration configuration)
        {
            _db = new NpgsqlConnection(configuration.GetConnectionString("PostgreSQL"));
        }

        public async Task<DiscordGuildDetailsDto?> Handle(GetDiscordGuildDetailsQuery request, CancellationToken cancellationToken)
        {
            return (await _db.QueryAsync<DiscordGuildDetailsDto>(
                $@"SELECT 
                    ""{nameof(RegisteredDiscordGuild)}"".""{nameof(RegisteredDiscordGuild.GuildId)}"" AS ""{nameof(DiscordGuildDetailsDto.Id)}"",
                    ""{nameof(RegisteredDiscordGuild.Name)}"" AS ""{nameof(DiscordGuildDetailsDto.Name)}"",
                    ""{nameof(RegisteredDiscordGuild.OwnerId)}"" AS ""{nameof(DiscordGuildDetailsDto.OwnerId)}"",
                    ""{nameof(GuildMessageCommands.MessageCommandPrefix)}"" AS ""{nameof(DiscordGuildDetailsDto.MessageCommandPrefix)}"" 
                FROM ""{ApplicationDbContext.DISCORD_SCHEMA}"".""{nameof(RegisteredDiscordGuild)}"" 
                LEFT JOIN ""{ApplicationDbContext.DISCORD_SCHEMA}"".""{nameof(GuildMessageCommands)}"" 
                    ON ""{nameof(RegisteredDiscordGuild)}"".""{nameof(RegisteredDiscordGuild.GuildId)}"" = ""{nameof(GuildMessageCommands)}"".""{nameof(GuildMessageCommands.GuildId)}"" 
                WHERE ""{nameof(RegisteredDiscordGuild)}"".""{nameof(RegisteredDiscordGuild.GuildId)}"" = @GuildId;", 
                new 
                {
                    // PostgreSQL doesn't support UInt64, so we need to cast them to decimals on queries
                    @GuildId = (decimal)request.GuildId
                })).SingleOrDefault();
        }
    }
}
