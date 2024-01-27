using DocuEye.Persistence;
using DocuEye.Web;

var logger = LoggerFactory.Create(config =>
{
    config.AddConsole();
}).CreateLogger("StartupLogger");

logger.LogInformation("Starting Up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Configuration.UseConfiguration<Program>(logger, true, new string[] { "DocuEye" });

    logger.LogInformation("Configure required services");
    builder.ConfigureServices(logger);
    var app = builder.Build();
    logger.LogInformation("Create db indexes");
    await app.Services.GetRequiredService<MongoDBContext>().CreateIndexes();
    logger.LogInformation("Configure application pipeline");
    app.ConfigurePipeline();
    app.Run();
}
catch (Exception ex)
{
    logger.LogCritical(ex, "Startup Error");
}
finally
{
    logger.LogInformation("Shutdown complette");
}
