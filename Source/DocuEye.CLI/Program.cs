using CommandLine;
using DocuEye.CLI;
using DocuEye.CLI.ApiClient;
using DocuEye.CLI.Application.Services.ImportWorkspace;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;


var logger = LoggerFactory.Create(config =>
{
    config.AddConsole();
}).CreateLogger("StartupLogger");

await Parser.Default.ParseArguments<CommandLineOptions>(args).MapResult(async (options) =>
{
    HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
    builder.Services.AddHttpClient<IDocuEyeApiClient, DocuEyeApiClient>(
        o =>
        {
            o.BaseAddress = new Uri(options.DocueyeAddress);
            o.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", options.AdminToken);
        }
    );

    builder.Services.AddLogging(builder => builder.AddConsole());

    builder.Services.AddTransient<IImportWorkspaceService, ImportWorkspaceService>();
    using IHost host = builder.Build();

    if (options.Import == "workspace")
    {
        var importService = host.Services.GetRequiredService<IImportWorkspaceService>();
        var parameters = new ImportWorkspaceParameters(
            options.ImportKey,
            options.WorkspaceFile,
            options.WorkspaceId,
            options.SourceLink);
        var result = await importService.Import(parameters);
        if(!result)
        {
            Environment.ExitCode = -1;
        }
    }
    else
    {
        Environment.ExitCode = -1;
        logger.LogError("--import value {0} is not valid", options.Import);
    }

}, errors => Task.FromResult(-1));





