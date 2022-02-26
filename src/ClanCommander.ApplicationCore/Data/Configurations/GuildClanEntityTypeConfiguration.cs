namespace ClanCommander.ApplicationCore.Data.Configurations;

internal class GuildClanEntityTypeConfiguration : IEntityTypeConfiguration<GuildClan>
{
    public void Configure(EntityTypeBuilder<GuildClan> builder)
    {
        builder.ToTable(nameof(GuildClan), ApplicationDbContext.DISCORDCLASHOFCLANS_SCHEMA);

        builder.HasKey(c => c.Id);

        builder.HasAlternateKey(c => c.ClanId);

        builder.Ignore(c => c.DomainEvents);

        builder.Property(c => c.Id)
            .UseHiLo("guild_clan_seq", ApplicationDbContext.DISCORDCLASHOFCLANS_SCHEMA);

        builder.Property(c => c.ClanId)
            .HasConversion(new ClanIdValueConverter());

        builder.Property(c => c.GuildId)
            .HasConversion(new DiscordGuildIdValueConverter());

        var navigation = builder.Metadata.FindNavigation(nameof(GuildClan.Members));

        // DDD Patterns comment:
        // Set as field to access the ClanMember collection property through its field
        navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasOne<RegisteredDiscordGuild>()
            .WithMany()
            .HasForeignKey(clan => clan.GuildId)
            .HasPrincipalKey(g => g.GuildId);

        builder.OwnsMany(c => c.Members, clanMember =>
        {
            clanMember.WithOwner().HasForeignKey("ClanId");

            clanMember.ToTable(nameof(GuildClanMember), ApplicationDbContext.DISCORDCLASHOFCLANS_SCHEMA);

            clanMember.HasKey(cm => new { cm.MemberId, cm.UserId });

            clanMember.Ignore(cm => cm.DomainEvents);

            clanMember.Property(cm => cm.MemberId)
                .HasConversion(new PlayerIdValueConverter());

            clanMember.Property(cm => cm.UserId)
                .HasConversion(new DiscordUserIdValueConverter());

            clanMember.Property(cm => cm.ClanRole)
                .HasConversion(new ClanMemberRoleValueConverter());

            clanMember.HasOne<DiscordUser>()
                .WithMany()
                .HasForeignKey(cm => cm.UserId)
                .HasPrincipalKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
