// <auto-generated />
using ClanCommander.ApplicationCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ClanCommander.ApplicationCore.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220424163112_FixRegisteredGuildAndMessageCommandsRelationship")]
    partial class FixRegisteredGuildAndMessageCommandsRelationship
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.HasSequence("coc_account_seq", "discord_coc")
                .IncrementsBy(10);

            modelBuilder.HasSequence("discord_user_seq", "discord")
                .IncrementsBy(10);

            modelBuilder.HasSequence("guild_clan_seq", "discord_coc")
                .IncrementsBy(10);

            modelBuilder.HasSequence("guild_message_commands_seq", "discord")
                .IncrementsBy(10);

            modelBuilder.HasSequence("registered_discord_guild_seq", "discord")
                .IncrementsBy(10);

            modelBuilder.Entity("ClanCommander.ApplicationCore.Entities.Discord.Guilds.RegisteredDiscordGuild", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "registered_discord_guild_seq", "discord");

                    b.Property<ulong>("GuildId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<ulong>("OwnerId")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("Id");

                    b.ToTable("RegisteredDiscordGuild", "discord");
                });

            modelBuilder.Entity("ClanCommander.ApplicationCore.Entities.Discord.MessageCommands.GuildMessageCommands", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "guild_message_commands_seq", "discord");

                    b.Property<ulong>("GuildId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("MessageCommandPrefix")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("GuildMessageCommands", "discord");
                });

            modelBuilder.Entity("ClanCommander.ApplicationCore.Entities.Discord.Users.DiscordUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "discord_user_seq", "discord");

                    b.Property<ulong>("UserId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DiscordUser", "discord");
                });

            modelBuilder.Entity("ClanCommander.ApplicationCore.Entities.DiscordClashOfClans.Clans.GuildClan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "guild_clan_seq", "discord_coc");

                    b.Property<string>("ClanId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("DiscordRoleId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<ulong>("GuildId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasAlternateKey("ClanId");

                    b.HasIndex("GuildId");

                    b.ToTable("GuildClan", "discord_coc");
                });

            modelBuilder.Entity("ClanCommander.ApplicationCore.Entities.DiscordClashOfClans.Users.ClashOfClansAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "coc_account_seq", "discord_coc");

                    b.Property<string>("AccountId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<ulong>("UserId")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ClashOfClansAccount", "discord_coc");
                });

            modelBuilder.Entity("ClanCommander.ApplicationCore.Entities.DiscordClashOfClans.Clans.GuildClan", b =>
                {
                    b.HasOne("ClanCommander.ApplicationCore.Entities.Discord.Guilds.RegisteredDiscordGuild", null)
                        .WithMany()
                        .HasForeignKey("GuildId")
                        .HasPrincipalKey("GuildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("ClanCommander.ApplicationCore.Entities.DiscordClashOfClans.Clans.GuildClanMember", "Members", b1 =>
                        {
                            b1.Property<string>("MemberId")
                                .HasColumnType("text");

                            b1.Property<ulong>("UserId")
                                .HasColumnType("numeric(20,0)");

                            b1.Property<int>("ClanId")
                                .HasColumnType("integer");

                            b1.Property<int>("Id")
                                .HasColumnType("integer");

                            b1.HasKey("MemberId", "UserId");

                            b1.HasIndex("ClanId");

                            b1.HasIndex("UserId");

                            b1.ToTable("GuildClanMember", "discord_coc");

                            b1.WithOwner()
                                .HasForeignKey("ClanId");

                            b1.HasOne("ClanCommander.ApplicationCore.Entities.Discord.Users.DiscordUser", null)
                                .WithMany()
                                .HasForeignKey("UserId")
                                .HasPrincipalKey("UserId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();
                        });

                    b.Navigation("Members");
                });

            modelBuilder.Entity("ClanCommander.ApplicationCore.Entities.DiscordClashOfClans.Users.ClashOfClansAccount", b =>
                {
                    b.HasOne("ClanCommander.ApplicationCore.Entities.Discord.Users.DiscordUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasPrincipalKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
