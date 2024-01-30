using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


var logger = LoggerFactory.Create(config =>
{
    config.AddConsole();
}).CreateLogger("StartupLogger");

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Configuration.AddCommandLine(args);

//

using IHost host = builder.Build();
bool isImport = false;
Boolean.TryParse(builder.Configuration["import"], out isImport);

logger.LogInformation(builder.Configuration["adminToken"]);



//await host.RunAsync();




