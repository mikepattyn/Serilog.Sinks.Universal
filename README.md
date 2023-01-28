Example below
```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Universal.Serilog.Sinks;
using Universal.Serilog.Sinks.Factories;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");
builder.Services.Configure<UniversalLoggerConfiguration>(builder.Configuration.GetSection(nameof(UniversalLoggerConfiguration)));
builder.Services.AddSingleton<IUniversalLoggerFactory, UniversalSlackLoggerFactory>();
builder.Services.AddSingleton<IUniversalLoggerFactory, UniversalDiscordLoggerFactory>();
builder.Services.AddSingleton<IUniversalLoggerFactory, UniversalKibanaLoggerFactory>();
var app = builder.Build();

app.MapGet("/", ([FromServices] IEnumerable<IUniversalLoggerFactory> LoggerFactories, [FromServices] IOptions<UniversalLoggerConfiguration> Configuration) =>
{
    var properties = new Dictionary<string, string>
    {
        { "Tag", $"{Configuration.Value.KibanaTag}.{Configuration.Value.Environment}" },
        { "User", "Mike" },
        { "Date", DateTime.UtcNow.ToLongDateString() },
        { "Message", "Lorem ipsum dolor sit amet." }
    };
    
    LoggerFactories.Single(x => x.CanHandle(UniversalPlatform.Slack))
        .Create("testen", properties).Information("Ah yeet, cva?"); // 
    LoggerFactories.Single(x => x.CanHandle(UniversalPlatform.Discord))
        .Create("testen", properties).Information("Ah yeet, cva?"); // 
    LoggerFactories.Single(x => x.CanHandle(UniversalPlatform.Kibana))
        .Create("testen", properties).Information("Ah yeet, cva?"); // 
});

app.Run();
```