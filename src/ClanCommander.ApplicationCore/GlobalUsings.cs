global using ClanCommander.ApplicationCore.Extensions;
global using ClanCommander.ApplicationCore.Entities.Shared;
global using ClanCommander.ApplicationCore.Entities.Guild;
global using ClanCommander.ApplicationCore.Entities.Guild.Events;
global using ClanCommander.ApplicationCore.Entities.MessageCommands;
global using ClanCommander.ApplicationCore.Entities.ClashOfClans;
global using ClanCommander.ApplicationCore.Entities.ClashOfClans.Events;
global using ClanCommander.ApplicationCore.Entities.Users;
global using ClanCommander.ApplicationCore.Entities.Users.Events;
global using ClanCommander.ApplicationCore.Entities.GuildEntry.ClashOfClans;
global using ClanCommander.ApplicationCore.Entities.GuildEntry.ClashOfClans.Events;
global using ClanCommander.ApplicationCore.Entities.GuildEntry.ClashOfClans.Interview.Events;
global using ClanCommander.ApplicationCore.Entities.Logging;
global using ClanCommander.ApplicationCore.Entities.Greeting;
global using ClanCommander.ApplicationCore.Data;
global using ClanCommander.ApplicationCore.Data.ValueConverters;
global using ClanCommander.ApplicationCore.Interfaces;
global using ClanCommander.ApplicationCore.Services;
global using ClanCommander.ApplicationCore.Constants;
global using ClanCommander.ApplicationCore.Exceptions;

global using ClanCommander.SharedKernal.Interfaces;
global using ClanCommander.SharedKernel.Models;

global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text;
global using System.Linq.Expressions;
global using System.Threading.Tasks;
global using System.Text.RegularExpressions;

global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

global using Discord;
global using Discord.Commands;
global using Discord.Interactions;
global using Discord.WebSocket;

global using ClashOfClans;
global using ClashOfClans.Models;

global using Google.Apis.Auth.OAuth2;
global using Google.Apis.Gmail.v1;
global using Google.Apis.Services;
global using Google.Apis.Util.Store;

global using Ardalis.GuardClauses;
global using Ardalis.SmartEnum;

global using MediatR;