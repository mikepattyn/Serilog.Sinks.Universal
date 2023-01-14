using Microsoft.Extensions.Options;
using Serilog;
using Universal.Serilog.Sinks.Dependencies.Serilog.Sinks.Discord;

namespace Universal.Serilog.Sinks.Factories;
public record UniversalDiscordLoggerFactory(IOptions<UniversalLoggerConfiguration> Configuration) : UniversalLoggerFactory(Configuration)
{
    public override UniversalPlatform Platform => UniversalPlatform.Discord;

    public override ILogger Create(string channelName, Dictionary<string, string> properties)
    {
        if (Loggers.ContainsKey(channelName))
            return Loggers[channelName];

        var loggerConfiguration = new LoggerConfiguration().WriteTo.Discord(Configuration.Value.DiscordWebhookId, Configuration.Value.DiscordWebhookToken, properties);
        Loggers.TryAdd(channelName, loggerConfiguration.CreateLogger());

        return base.Create(channelName, properties);
    }
}