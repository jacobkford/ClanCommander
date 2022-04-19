﻿global using ClanCommander.ApplicationCore.Data;
global using ClanCommander.ApplicationCore.Data.ValueConverters;
global using ClanCommander.ApplicationCore.Entities.ClashOfClans;
global using ClanCommander.ApplicationCore.Entities.Discord.Guilds;
global using ClanCommander.ApplicationCore.Entities.Discord.Users;
global using ClanCommander.ApplicationCore.Entities.DiscordClashOfClans.Clans;
global using ClanCommander.ApplicationCore.Entities.DiscordClashOfClans.Users;
global using ClanCommander.ApplicationCore.Entities.MessageCommands;
global using ClanCommander.ApplicationCore.Features.Discord.Guilds.ClientEvents;
global using ClanCommander.ApplicationCore.Features.Discord.Guilds.Commands.ChangeGuildMessageCommandsPrefix;
global using ClanCommander.ApplicationCore.Features.Discord.Guilds.Queries.GetGuildDetails;
global using ClanCommander.ApplicationCore.Features.Discord.Users.ClientEvents;
global using ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Clans.Commands.AddClanMemberToGuild;
global using ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Clans.Queries.GetClanDetails;
global using ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Clans.Queries.GetClanRoster;
global using ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Clans.Queries.GetClanRosterComposition;
global using ClanCommander.ApplicationCore.Interfaces;
global using ClanCommander.ApplicationCore.Services;
global using ClanCommander.IntegrationTests.Mocks;
global using Dapper;
global using FluentAssertions;
global using MediatR;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Caching.Distributed;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Reflection;
global using System.Threading.Tasks;
global using Xunit;
global using static FluentAssertions.FluentActions;
