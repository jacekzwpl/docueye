using DocuEye.CLI.ApiClient;
using DocuEye.CLI.Application.Services.Compatibility;
using DocuEye.CLI.Application.Services.DeleteDocumentationFile;
using DocuEye.CLI.Application.Services.DeleteWorkspace;
using DocuEye.CLI.Application.Services.DSL;
using DocuEye.CLI.Application.Services.ImportDocumentationFile;
using DocuEye.CLI.Application.Services.ImportWorkspace;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;

namespace DocuEye.CLI.Hosting
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

            builder.Logging.SetMinimumLevel(LogLevel.Error);
            builder.Logging.AddFilter((provider, category, level) =>
            {
                if (category != null && category.StartsWith("DocuEye"))
                    return level >= LogLevel.Information;
                // default
                return level >= LogLevel.Error;
            });

            builder.Services.AddHttpClient<IDocuEyeApiClient, DocuEyeApiClient>(
                o =>
                {
                    o.BaseAddress = new Uri(options.DocueyeAddress);
                    o.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", options.AdminToken);
                }
            );

            builder.Services.AddTransient<ICompatibilityCheckService, CompatibilityCheckService>();
            builder.Services.AddTransient<IWorkspaceParserService, WorkspaceParserService>();
            builder.Services.AddTransient<IImportWorkspaceService, ImportWorkspaceService>();
            builder.Services.AddTransient<IDeleteWorkspaceService, DeleteWorkspaceService>();
            builder.Services.AddTransient<IImportDocumentationFileService, ImportDocumentationFileService>();
            builder.Services.AddTransient<IDeleteDocumentationFileService, DeleteDocumentationFileService>();
            return builder.Build();
        }
    }
}
