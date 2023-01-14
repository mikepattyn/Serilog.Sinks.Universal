using System.Collections.Concurrent;
using Microsoft.Extensions.Options;
using Serilog;

namespace Universal.Serilog.Sinks;

public abstract record UniversalLoggerFactory(IOptions<UniversalLoggerConfiguration> Configuration) : IUniversalLoggerFactory
{
    public abstract UniversalPlatform Platform { get; }
    public ConcurrentDictionary<string, ILogger> Loggers = new();
    public virtual ILogger Create(string channelName, Dictionary<string, string> properties)
    {
        return Loggers[channelName];
    }

    public bool CanHandle(UniversalPlatform platform)
    {
        return Platform == platform;
    }
}
