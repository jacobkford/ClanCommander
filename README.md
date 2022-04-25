<h1 align="center">
  <br>
  Clan Commander
  <br>
</h1>

<h4 align="center">Discord bot to manage clans from the game Clash Of Clans.</h4>

<p align="center">
  <a href="#key-features">Key Features</a> •
  <a href="#upcoming-features">Upcoming Features</a> •
  <a href="#how-to-use">How To Use</a>
</p>

## Key Features

* Discord Server Mangement
  - Change prefix that triggers commands
* Discord Server Clans
  - Add & remove clans from your server
  - Add users to your clan roster
* Link Clash Of Clans account to User
* Cross platform hosting with the .NET runtime

## Upcoming Features

* Moderation commands/tools
* User welcome system
* Discord channel event logging
* Clash Of Clans account & clan verification
  - Block other users/servers linking clans & accounts unless they have api key
* Clan Entry/Recruitment System
  - Custom entry requirement strategies
  - Auto role & account linking management
  - Custom messages / instructions
* In-game event notifications
* Clan war management
* Clan War Leagues management

## How To Use

To download this application, I recommend cloning it via [Git](https://git-scm.com), which you'll need installed on your computer. From your command line:

```bash
# Clone this repository
$ git clone https://github.com/jacobkford/ClanCommander
```

You will need two different API tokens to run this application, which are the following:

* [Discord Bot Token](https://www.writebots.com/discord-bot-token/)
* [Clash Of Clans API Token](https://developer.clashofclans.com/#/)

There are two ways you can run this application, either using [Docker](https://www.docker.com) *(quicker)* or manually installing all the dependancies and running it via the .NET runtime.

### Docker

Go into the ClanCommander project folder you've just downloaded, and open the [docker-compose.yml](https://github.com/jacobkford/ClanCommander/blob/master/docker-compose.yml) file, and update the environment credentials highlighed below:

```yaml
# clancommander.discordbot service
environment:
  - Discord:BotToken= # add bot token here
  - Discord:BotOwnerId=	# add your discord user id
  - Discord:DevGuildId= # add dev/test guild id
  - ConnectionStrings:PostgreSQL= # add db connection string here
  - ConnectionStrings:Redis= # add redis connection string here
  - ClashOfClansAPI:Token= # add Clash Of Clans api token here

# db service
environment:
  - POSTGRES_PASSWORD= # add db password here
  - POSTGRES_USER= # add db user name here
  - POSTGRES_DB= # add db name here
ports:
  - 5432:5432 # add postgresql port number here

# redis-server service
ports:
  - 6379:6379 # add redis port number here
```

After you've setup all the environment credentials, you will need to open your command line and do the following:

```bash
# Go into the ClanCommander repository
$ cd ClanCommander

# Run the application
$ docker-compose run
```

### .NET Runtime

To run this application without docker on your machine, you will need to download & install the following:

* [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
* [PostgreSQL](https://www.postgresql.org/download/)
* [Redis](https://redis.io/download/)

You will also need two different API tokens to run this application, which are the following:

* [Discord Bot Token](https://www.writebots.com/discord-bot-token/)
* [Clash Of Clans API Token](https://developer.clashofclans.com/#/)

After you've installed & obtained all the recruitments, we will now need to update the following enviroment credentials in our [appsettings.json](https://github.com/jacobkford/ClanCommander/blob/master/src/ClanCommander.DiscordBot/appsettings.json) file:

```json
{
  "Discord": {
    "BotToken": "", # add bot token here
    "BotOwnerId": 0, # add your discord user id
    "DevGuildId": 0 # add dev/test guild id
  },
  "ConnectionStrings": {
    "PostgreSQL": "", # add db connection string here
    "Redis": "" # add redis connection string here
  },
  "ClashOfClansAPI": {
    "Token":  "" # add Clash Of Clans api token here
  }
}
```

After you've setup all the environment credentials, you will need to open your command line and do the following:

```bash
# Go into the ClanCommander repository
$ cd ClanCommander

# Build application
$ dotnet build .\src\ClanCommander.DiscordBot\

# Run application
$ dotnet run .\src\ClanCommander.DiscordBot\
```
