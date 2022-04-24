namespace ClanCommander.ApplicationCore.Data.Configurations;

internal class RegisteredDiscordGuildEntityTypeConfiguration : IEntityTypeConfiguration<RegisteredDiscordGuild>
{
    public void Configure(EntityTypeBuilder<RegisteredDiscordGuild> builder)
    {
        builder.ToTable(nameof(RegisteredDiscordGuild), ApplicationDbContext.DISCORD_SCHEMA);

        builder.HasKey(rdb => rdb.Id);

        builder.Ignore(rdb => rdb.DomainEvents);

        builder.Property(rdb => rdb.Id)
            .UseHiLo("registered_discord_guild_seq", ApplicationDbContext.DISCORD_SCHEMA);

        builder.HasAlternateKey(rdg => rdg.GuildId);

        builder.Property(rdg => rdg.GuildId)
            .HasConversion(new DiscordGuildIdValueConverter())
            .IsRequired(true);

        builder.Property(rdg => rdg.OwnerId)
            .HasConversion(new DiscordUserIdValueConverter())
            .IsRequired(true);

        builder.Property(rdg => rdg.Name)
            .IsRequired(true);

    }
}
