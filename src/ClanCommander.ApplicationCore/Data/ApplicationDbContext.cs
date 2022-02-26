namespace ClanCommander.ApplicationCore.Data;

internal class ApplicationDbContext : DbContext
{
    #region Discord Context
    public const string DISCORD_SCHEMA = "discord";
    public DbSet<RegisteredDiscordGuild> DiscordGuilds { get; set; }
    public DbSet<GuildMessageCommands> MessageCommandsConfigurations { get; set; }
    public DbSet<DiscordUser> DiscordUsers { get; set; }
    #endregion

    #region Discord Clash Of Clans Context
    public const string DISCORDCLASHOFCLANS_SCHEMA = "discord_coc";
    public DbSet<GuildClan> GuildClans { get; set; }
    public DbSet<GuildClanMember> GuildClanMembers { get; set; }
    public DbSet<GuildClanMemberRole> GuildClanMemberRoles { get; set; }
    public DbSet<ClashOfClansAccount> ClashOfClansUserAccounts { get; set; }
    #endregion

    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}

