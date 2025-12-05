using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var originalColor = Console.BackgroundColor;
                Console.BackgroundColor = ConsoleColor.Red;
                textWriter.Write("fail:");
                Console.BackgroundColor = originalColor;
            }

            //CustomLogicGoesHere(textWriter);
            textWriter.WriteLine(message);
        }
    }
}
