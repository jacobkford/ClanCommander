namespace ClanCommander.ApplicationCore.Data.Configurations;

internal class GuildMessageCommandsEntityTypeConfiguration : IEntityTypeConfiguration<GuildMessageCommands>
{
    public void Configure(EntityTypeBuilder<GuildMessageCommands> builder)
    {
        builder.ToTable(nameof(GuildMessageCommands), ApplicationDbContext.DISCORD_SCHEMA);

        builder.HasKey(gmc => gmc.Id);

        builder.Ignore(gmc => gmc.DomainEvents);

        builder.Property(gmc => gmc.Id)
            .UseHiLo("guild_message_commands_seq", ApplicationDbContext.DISCORD_SCHEMA);

        builder.Property(gmc => gmc.GuildId)
            .HasConversion(new DiscordGuildIdValueConverter())
            .IsRequired(true);

        builder.Property(gmc => gmc.MessageCommandPrefix)
            .IsRequired(true);
    }
}
