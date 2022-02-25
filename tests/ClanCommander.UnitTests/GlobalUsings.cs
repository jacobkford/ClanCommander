﻿global using ClanCommander.ApplicationCore.Constants;
global using ClanCommander.ApplicationCore.Data;
global using ClanCommander.ApplicationCore.Entities.ClashOfClans;
global using ClanCommander.ApplicationCore.Entities.ClashOfClans.Events;
global using ClanCommander.ApplicationCore.Entities.Discord.Guilds;
global using ClanCommander.ApplicationCore.Entities.DiscordClashOfClans.Clans;
global using ClanCommander.ApplicationCore.Entities.DiscordClashOfClans.Users;
global using ClanCommander.ApplicationCore.Entities.DiscordClashOfClans.Users.Events;
global using ClanCommander.ApplicationCore.Entities.Discord.Guilds.Events;
global using ClanCommander.ApplicationCore.Entities.Discord.Users;
global using ClanCommander.ApplicationCore.Entities.Discord.Users.Events;
global using ClanCommander.ApplicationCore.Entities.MessageCommands;
global using ClanCommander.ApplicationCore.Entities.Shared;
global using ClanCommander.ApplicationCore.Extensions;
global using ClanCommander.ApplicationCore.Interfaces;
global using ClanCommander.ApplicationCore.Services;
global using ClanCommander.SharedKernal.Interfaces;
global using ClanCommander.SharedKernel.Models;
global using FluentAssertions;
global using System;
global using System.Collections.Generic;
global using Xunit;
global using static FluentAssertions.FluentActions;
