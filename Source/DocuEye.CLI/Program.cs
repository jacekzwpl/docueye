using CommandLine;
using DocuEye.CLI;
using DocuEye.CLI.ApiClient;
using DocuEye.CLI.Application.Services.ImportWorkspace;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;


var logger = LoggerFactory.Create(config =>
{
    config.AddConsole();
}).CreateLogger("StartupLogger");


//builder.Configuration.AddCommandLine(args);

Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsed<CommandLineOptions>(options =>
{
    HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
    builder.Services.AddHttpClient<IDocuEyeApiClient, DocuEyeApiClient>(
    o => {
        o.BaseAddress = new Uri(options.DocueyeAddress);
        o.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", options.AdminToken);
    }
);

    builder.Services.AddLogging(builder => builder.AddConsole());

    builder.Services.AddTransient<IImportWorkspaceService, ImportWorkspaceService>();
    using IHost host = builder.Build();

});





//bool isImport = false;
//Boolean.TryParse(builder.Configuration["import"], out isImport);

//logger.LogInformation(builder.Configuration["adminToken"]);



//var importService = host.Services.GetRequiredService<IImportWorkspaceService>();
/*
var parameters = new ImportWorkspaceParameters(
    Guid.NewGuid().ToString(), 
    "", 
    "");
await importService.Import(parameters);*/


//await host.RunAsync();




