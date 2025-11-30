using DocuEye.CLI.ApiClient;
using DocuEye.CLI.Application.Services.DSL;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace DocuEye.CLI
{
    public class CliHostBuilder
    {
        public IHost Build(CliHostOptions options) {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder();

            
            builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddConsole(consoleOptions =>
            {
                consoleOptions.FormatterName = "customName";
                
            })
            .AddConsoleFormatter<CliLogFormatter, CliLogFormatterOptions>());

            builder.Logging.SetMinimumLevel(LogLevel.Information);
            builder.Services.AddHttpClient<IDocuEyeApiClient, DocuEyeApiClient>(
                o =>
                {
                    o.BaseAddress = new Uri(options.DocueyeAddress);
                    o.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", options.AdminToken);
                }
            );
            builder.Services.AddTransient<IWorkspaceParserService, WorkspaceParserService>();
            return builder.Build();
        }
    }
}
