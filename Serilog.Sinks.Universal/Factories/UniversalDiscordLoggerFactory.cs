using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Discord;
using System.Collections.Concurrent;

namespace Universal.Serilog.Sinks.Factories;
public record UniversalDiscordLoggerFactory(IOptions<UniversalLoggerConfiguration> Configuration) : UniversalLoggerFactory(Configuration)
{
    public override UniversalPlatform Platform => UniversalPlatform.Discord;

    public override ILogger Create(string channelName, Dictionary<string, string> properties)
    {
        if (Loggers.ContainsKey(channelName))
            Loggers = new ConcurrentDictionary<string, ILogger>();

        var loggerConfiguration = new LoggerConfiguration().WriteTo.Discord(Configuration.Value.DiscordWebhookId, Configuration.Value.DiscordWebhookToken, null, LogEventLevel.Verbose, properties);

        Loggers.TryAdd(channelName, loggerConfiguration.CreateLogger());

        return base.Create(channelName, properties);
    }
}