using DocuEye.CLI;
using Microsoft.Extensions.Logging;
using System.CommandLine;


var logger = LoggerFactory.Create(config =>
{
    config.AddConsole();
}).CreateLogger("StartupLogger");

var commonOptions = new CommandLineCommonOptions();

// Create root command
RootCommand rootCommand = new("Test opis");
// Create workspace command
var workspaceCommand = new WorkspaceCommand();
workspaceCommand.Subcommands.Add(new WorkspaceImportCommand(commonOptions));
workspaceCommand.Subcommands.Add(new WorkspaceDeleteCommand(commonOptions));
// Create openapi command
var openapiCommand = new OpenapiCommand();
openapiCommand.Subcommands.Add(new OpenapiImportCommand(commonOptions));
// Add subcommands to root command
rootCommand.Subcommands.Add(workspaceCommand);
rootCommand.Subcommands.Add(openapiCommand);

return await rootCommand.Parse(args).InvokeAsync();


/*
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

    var mappingConfig = new MapperConfiguration(mc =>
    {

        mc.AddProfile(new StructurizrDocumentationToApiModelMappingProfile());
        mc.AddProfile(new StructurizrModelToApiMappingProfile());
        mc.AddProfile(new StructurizrViewsToApiMappingProfile());
    });

    IMapper mapper = mappingConfig.CreateMapper();
    builder.Services.AddSingleton(mapper);

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

*/



