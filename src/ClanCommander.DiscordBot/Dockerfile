#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/ClanCommander.DiscordBot/ClanCommander.DiscordBot.csproj", "src/ClanCommander.DiscordBot/"]
COPY ["src/ClanCommander.ApplicationCore/ClanCommander.ApplicationCore.csproj", "src/ClanCommander.ApplicationCore/"]
COPY ["src/ClanCommander.SharedKernel/ClanCommander.SharedKernel.csproj", "src/ClanCommander.SharedKernel/"]
RUN dotnet restore "src/ClanCommander.DiscordBot/ClanCommander.DiscordBot.csproj"
COPY . .
WORKDIR "/src/src/ClanCommander.DiscordBot"
RUN dotnet build "ClanCommander.DiscordBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClanCommander.DiscordBot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClanCommander.DiscordBot.dll"]