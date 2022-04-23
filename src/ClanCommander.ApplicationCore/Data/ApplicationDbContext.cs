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
    public DbSet<ClashOfClansAccount> ClashOfClansUserAccounts { get; set; }
    #endregion

    private readonly IMediator _mediator;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public ApplicationDbContext(DbContextOptions options, IMediator mediator) : base(options) 
    { 
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        var domainEntities = this.ChangeTracker.Entries<Entity>()
            .Where(x => x.Entity.DomainEvents is not null && x.Entity.DomainEvents.Any());

        var domainEvents = domainEntities.SelectMany(x => x.Entity.DomainEvents!).ToList();

        domainEntities.ToList().ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await _mediator.Publish(domainEvent, cancellationToken);

        var result = await base.SaveChangesAsync(cancellationToken);

        return true;
    }
}

