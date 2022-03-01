Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting host");

    var host = Host.CreateDefaultBuilder(args)
        .UseSerilog()
        .ConfigureDiscordHost((context, config) =>
        {
            config.SocketConfig = new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose,
                AlwaysDownloadUsers = true,
                MessageCacheSize = 200
            };

            config.Token = context.Configuration["Discord:BotToken"];

            //Use this to configure a custom format for Client/CommandService logging if needed. The default is below and should be suitable for Serilog usage
            config.LogFormat = (message, exception) => $"{message.Source}: {message.Message}";
        })
        .UseCommandService((context, config) =>
        {
            config.LogLevel = LogSeverity.Info;
            config.DefaultRunMode = Discord.Commands.RunMode.Async;
        })
        .UseInteractionService((context, config) =>
        {
            config.LogLevel = LogSeverity.Info;
            config.UseCompiledLambda = true;
        })
        .ConfigureServices((context, services) =>
        {
            services.AddHostedService<CommandHandler>();
            services.AddHostedService<InteractionHandler>();
            services.AddHostedService<ReadyService>();
            services.AddSingleton<InteractiveService>();
            services.AddApplicationCore(context.Configuration);
        }).Build();
    
    await host.SeedDatabaseAsync();
    await host.RunAsync();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}
