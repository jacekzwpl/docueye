using AutoMapper;
using DocuEye.CLI.ApiClient;
using DocuEye.CLI.Application.Services.DSL;
using DocuEye.CLI.Application.Services.ImportWorkspace;
using DocuEye.Structurizr.Model.Exploders.Mappings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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

            builder.Logging.SetMinimumLevel(LogLevel.Information);
            builder.Services.AddHttpClient<IDocuEyeApiClient, DocuEyeApiClient>(
                o =>
                {
                    o.BaseAddress = new Uri(options.DocueyeAddress);
                    o.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", options.AdminToken);
                }
            );

            var mappingConfig = new MapperConfiguration(mc =>
            {

                mc.AddProfile(new StructurizrDocumentationToApiModelMappingProfile());
                mc.AddProfile(new StructurizrModelToApiMappingProfile());
                mc.AddProfile(new StructurizrViewsToApiMappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);


            builder.Services.AddTransient<IWorkspaceParserService, WorkspaceParserService>();
            builder.Services.AddTransient<IImportWorkspaceService, ImportWorkspaceService>();
            return builder.Build();
        }
    }
}
