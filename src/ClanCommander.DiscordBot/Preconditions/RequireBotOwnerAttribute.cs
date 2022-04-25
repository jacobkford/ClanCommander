namespace ClanCommander.DiscordBot.Preconditions;

public class RequireBotOwnerAttribute : Discord.Commands.PreconditionAttribute
{
    public override Task<Discord.Commands.PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
    {
        var configuration = services.GetService<IConfiguration>();
        var ownerId = configuration.GetValue<ulong>("Discord:BotOwnerId");

        if (ownerId == context.User.Id)
            return Task.FromResult(Discord.Commands.PreconditionResult.FromSuccess());
        else
            return Task.FromResult(Discord.Commands.PreconditionResult.FromError("You must be the bot owner to run this command"));
    }
}
