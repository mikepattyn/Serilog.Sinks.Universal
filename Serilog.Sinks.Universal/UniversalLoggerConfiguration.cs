namespace Universal.Serilog.Sinks
{
    public class UniversalLoggerConfiguration
    {
        public bool IsProduction { get; set; }
        public ulong DiscordWebhookId { get; set; }
        public string DiscordWebhookToken { get; set; }
        public string KibanaTag { get; set; }
        public string KibanaUrl { get; set; }
        public string KibanaKey { get; set; }
        public string SlackWebhookUrl { get; set; }
        public string Environment => IsProduction ? "Production" : "Debug";
    }
}
