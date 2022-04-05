﻿global using ClanCommander.ApplicationCore;
global using ClanCommander.ApplicationCore.Features.Discord.Guilds.Commands.ChangeGuildMessageCommandsPrefix;
global using ClanCommander.ApplicationCore.Features.Discord.Guilds.Queries.GetGuildDetails;
global using ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Clans.Queries.GetClanDetails;
global using ClanCommander.ApplicationCore.Interfaces;
global using ClanCommander.DiscordBot.Services;
global using ClanCommander.DiscordBot.Extensions;
global using ClanCommander.SharedKernel.DiscordEmbedBuilders;
global using Discord;
global using Discord.Addons.Hosting;
global using Discord.Addons.Hosting.Util;
global using Discord.Commands;
global using Discord.Interactions;
global using Discord.WebSocket;
global using Fergun.Interactive;
global using MediatR;
global using Serilog;
global using Serilog.Events;
global using System;
global using System.Reflection;
global using System.Threading.Tasks;
global using ICommandResult = Discord.Commands.IResult;
global using IInteractionResult = Discord.Interactions.IResult;
