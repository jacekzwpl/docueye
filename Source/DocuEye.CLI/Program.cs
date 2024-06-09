using CommandLine;
using DocuEye.CLI;
using DocuEye.CLI.ApiClient;
using DocuEye.CLI.Application.Services.DeleteWorkspace;
using DocuEye.CLI.Application.Services.ImportOpenApiFile;
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
    builder.Services.AddTransient<IImportOpenApiFileService, ImportOpenApiFileService>();
    builder.Services.AddTransient<IDeleteWorkspaceService, DeleteWorkspaceService>();
    using IHost host = builder.Build();

    if (options.Import == "workspace")
    {
        if (string.IsNullOrEmpty(options.WorkspaceFile))
        {
            logger.LogError("workspaceFile is required for workspace import");
            Environment.ExitCode = -1;
            return;
        }
        var importService = host.Services.GetRequiredService<IImportWorkspaceService>();
        var parameters = new ImportWorkspaceParameters(
            options.ImportKey ?? Guid.NewGuid().ToString(),
            options.WorkspaceFile,
            options.WorkspaceId,
            options.SourceLink);
        var result = await importService.Import(parameters);
        if(!result)
        {
            Environment.ExitCode = -1;
        }
    }else if(options.Import == "openapi")
    {
        if (string.IsNullOrEmpty(options.ElementId) && string.IsNullOrEmpty(options.ElementDslId))
        {
            logger.LogError("elementId or elementDslId is required for openapi import");
            Environment.ExitCode = -1;
            return;
        }

        if (string.IsNullOrEmpty(options.WorkspaceId))
        {
            logger.LogError("workspaceId is required for openapi import");
            Environment.ExitCode = -1;
            return;
        }

        if (string.IsNullOrEmpty(options.OpenApiFile))
        {
            logger.LogError("openApiFile is required for openapi import");
            Environment.ExitCode = -1;
            return;
        }

        var importService = host.Services.GetRequiredService<IImportOpenApiFileService>();
        var parameters = new ImportOpenApiFileParameters()
        {
           ElementId = options.ElementId,
           ElementDslId = options.ElementDslId,
           WorkspaceId = options.WorkspaceId,
           OpenApiFile = options.OpenApiFile
        };
        var result = await importService.Import(parameters);
        if (!result)
        {
            Environment.ExitCode = -1;
        }
    }else if(options.Import == "delete")
    {
        if (string.IsNullOrEmpty(options.WorkspaceId))
        {
            logger.LogError("workspaceId is required for delete workspace operation");
            Environment.ExitCode = -1;
            return;
        }

        var importService = host.Services.GetRequiredService<IDeleteWorkspaceService>();
        var result = await importService.DeleteWorkspace(options.WorkspaceId);
        if (!result)
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





