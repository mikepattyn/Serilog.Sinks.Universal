using Serilog;

namespace Universal.Serilog.Sinks;

public interface IUniversalLoggerFactory
{
    UniversalPlatform Platform { get; }
    ILogger Create(string channelName, Dictionary<string, string> properties);
    bool CanHandle(UniversalPlatform platform);
}
