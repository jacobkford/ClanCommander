namespace ClanCommander.ApplicationCore.Data.Configurations;

internal class ClashOfClansAccountEntityTypeConfiguration : IEntityTypeConfiguration<ClashOfClansAccount>
{
    public void Configure(EntityTypeBuilder<ClashOfClansAccount> builder)
    {
        builder.ToTable(nameof(ClashOfClansAccount), ApplicationDbContext.DISCORDCLASHOFCLANS_SCHEMA);

        builder.HasKey(a => a.Id);

        builder.Ignore(a => a.DomainEvents);

        builder.Property(a => a.Id)
            .UseHiLo("coc_account_seq", ApplicationDbContext.DISCORDCLASHOFCLANS_SCHEMA);

        builder.Property(a => a.AccountId)
            .HasConversion(new PlayerIdValueConverter())
            .IsRequired(true);

        builder.Property(a => a.UserId)
            .HasConversion(new DiscordUserIdValueConverter())
            .IsRequired(true);

        builder.Property(a => a.Name)
            .IsRequired(true);

        builder.HasOne<DiscordUser>()
            .WithMany()
            .HasForeignKey(a => a.UserId)
            .HasPrincipalKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
