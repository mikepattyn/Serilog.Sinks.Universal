using Microsoft.Extensions.Options;
using Serilog.Sinks.Http;
using Serilog;

namespace Universal.Serilog.Sinks.Factories;
public record UniversalKibanaLoggerFactory(IOptions<UniversalLoggerConfiguration> Configuration) : UniversalLoggerFactory(Configuration)
{
    public override UniversalPlatform Platform => UniversalPlatform.Kibana;
    public override ILogger Create(string channelName, Dictionary<string, string> properties)
    {
        if (Loggers.ContainsKey(channelName))
            return Loggers[channelName];

        var loggerConfiguration = new LoggerConfiguration().Enrich.WithProperty("tag", Configuration.Value.KibanaTag).WriteTo.HttpSink(Configuration.Value.KibanaUrl, Configuration.Value.KibanaKey);
        if (Configuration?.Value != null && !Configuration.Value.IsProduction)
            loggerConfiguration = loggerConfiguration.Enrich.WithProperty("from", "DEBUG");

        Loggers.TryAdd(channelName, loggerConfiguration.CreateLogger());

        return base.Create(channelName, properties);
    }
}

