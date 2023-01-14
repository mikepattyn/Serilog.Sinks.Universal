using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Sinks.Slack;
using Serilog.Sinks.Slack.Models;

namespace Universal.Serilog.Sinks.Factories;
public record UniversalSlackLoggerFactory(IOptions<UniversalLoggerConfiguration> Configuration) : UniversalLoggerFactory(Configuration)
{
    public override UniversalPlatform Platform => UniversalPlatform.Slack;

    public override ILogger Create(string channelName, Dictionary<string, string> properties)
    {
        if (Loggers.ContainsKey(channelName))
            return Loggers[channelName];

        var options = BuildOptions(Configuration.Value.SlackWebhookUrl, channelName);
        var loggerConfiguration = new LoggerConfiguration().WriteTo.Slack(options);

        Loggers.TryAdd(channelName, loggerConfiguration.CreateLogger());

        return base.Create(channelName, properties);
    }

    private SlackSinkOptions BuildOptions(string webhookUrl, string channel)
    {
        return new SlackSinkOptions
        {
            WebHookUrl = webhookUrl,
            BatchSizeLimit = 10,
            CustomIcon = ":guardsman:",
            Period = TimeSpan.FromSeconds(1),
            ShowDefaultAttachments = false,
            ShowExceptionAttachments = true,
            CustomChannel = channel
        };
    }
}

public interface ILoggerFactory
{
    ILogger Configure();
}