namespace ClanCommander.ApplicationCore.Data.Configurations;

internal class DiscordUserEntityTypeConfiguration : IEntityTypeConfiguration<DiscordUser>
{
    public void Configure(EntityTypeBuilder<DiscordUser> builder)
    {
        builder.ToTable(nameof(DiscordUser), ApplicationDbContext.DISCORD_SCHEMA);

        builder.HasKey(u => u.Id);

        builder.Ignore(u => u.DomainEvents);

        builder.Property(u => u.Id)
            .UseHiLo("discord_user_seq", ApplicationDbContext.DISCORD_SCHEMA);

        builder.HasAlternateKey(user => user.UserId);

        builder.Property(user => user.UserId)
            .HasConversion(new DiscordUserIdValueConverter());

        builder.Property(user => user.Username)
            .IsRequired(true);
    }
}
