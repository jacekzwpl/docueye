using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace DocuEye.CLI.Hosting
{

    
    public class CliLogFormatter : ConsoleFormatter, IDisposable
    {
        private readonly IDisposable? _optionsReloadToken;
        private CliLogFormatterOptions _formatterOptions;

        public CliLogFormatter(IOptionsMonitor<CliLogFormatterOptions> options)
        // Case insensitive
        : base("customName") =>
        (_optionsReloadToken, _formatterOptions) =
            (options.OnChange(ReloadLoggerOptions), options.CurrentValue);

        private void ReloadLoggerOptions(CliLogFormatterOptions options) =>
            _formatterOptions = options;


        public void Dispose() => _optionsReloadToken?.Dispose();

        public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider? scopeProvider, TextWriter textWriter)
        {
            string? message =
            logEntry.Formatter?.Invoke(
                logEntry.State, logEntry.Exception);

            if (message is null)
            {
                return;
            }

            if(logEntry.LogLevel == LogLevel.Error || logEntry.LogLevel == LogLevel.Critical)
            {
                textWriter.Write("fail:");
            }
            else if(logEntry.LogLevel == LogLevel.Warning)
            {
                textWriter.Write("warn:");
            }
            else if(logEntry.LogLevel == LogLevel.Information)
            {
                textWriter.Write("info:");
            } else if(logEntry.LogLevel == LogLevel.Debug || logEntry.LogLevel == LogLevel.Trace)
            {
                textWriter.Write("debug:");
            }

            textWriter.WriteLine(message);
        }
    }
}
