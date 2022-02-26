namespace ClanCommander.ApplicationCore.Data.Configurations;

internal class GuildClanMemberRoleEntityTypeConfiguration : IEntityTypeConfiguration<GuildClanMemberRole>
{
    public void Configure(EntityTypeBuilder<GuildClanMemberRole> builder)
    {
        builder.ToTable(nameof(GuildClanMemberRole), ApplicationDbContext.DISCORDCLASHOFCLANS_SCHEMA);

        builder.HasKey(gcmr => gcmr.Id);

        builder.Ignore(gcmr => gcmr.DomainEvents);

        builder.Property(gcmr => gcmr.Id)
            .UseHiLo("guild_clan_member_role_seq", ApplicationDbContext.DISCORDCLASHOFCLANS_SCHEMA);

        builder.HasIndex(gcmr => new { gcmr.GuildId, gcmr.InGameRole })
            .IsUnique(true);

        builder.Property(gcmr => gcmr.GuildId)
            .HasConversion(new DiscordGuildIdValueConverter());

        builder.Property(gcmr => gcmr.DiscordRoleId)
            .IsRequired(true);

        builder.Property(gcmr => gcmr.InGameRole)
            .HasConversion(new ClanMemberRoleValueConverter());

        builder.HasOne<RegisteredDiscordGuild>()
            .WithMany()
            .HasForeignKey(gcmr => gcmr.GuildId)
            .HasPrincipalKey(g => g.GuildId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
