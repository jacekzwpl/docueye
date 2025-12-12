using DocuEye.CLI.Application.Services.Compatibility;
using DocuEye.CLI.Application.Services.ImportOpenApiFile;
using DocuEye.CLI.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;
using System.CommandLine.Parsing;

namespace DocuEye.CLI.Commands
{
    public class OpenapiImportCommand : Command
    {
        private Option<string> openApiImportFileOption;
        private Option<string> openApiImportElementIdOption;
        private Option<string> openApiImportElementDslIdOption;
        private Option<string> openApiImportWorkspaceIdOption;

        public OpenapiImportCommand() : base("import", "Imports or updates OpenAPI specification for given element.")
        {

            this.openApiImportFileOption = new("--file", "-f")
            {
                Description = "Path to openapi specification file for element.",
                Required = true
            };

            this.openApiImportElementIdOption = new("--element-id", "-e")
            {
                Description = "The ID of element for which this import is created. Required only if --element-dsl-id option is not set.",
            };

            this.openApiImportElementDslIdOption = new("--element-dsl-id", "-d")
            {
                Description = "The DSL ID of element for which this import is created. Required only if --element-id option is not set.",
            };

            this.openApiImportWorkspaceIdOption = new("--id", "-i")
            {
                Description = "The ID of the Workspace where element exists.",
                Required = true
            };

            this.Options.Add(CommandLineCommonOptions.DocueyeAddressOption);
            this.Options.Add(CommandLineCommonOptions.AdminTokenOption);
            this.Options.Add(this.openApiImportFileOption);
            this.Options.Add(this.openApiImportWorkspaceIdOption);
            this.Options.Add(this.openApiImportElementIdOption);
            this.Options.Add(this.openApiImportElementDslIdOption);
            this.SetAction(async parseResult => await this.Run(parseResult));
        }

        private async Task<int> Run(ParseResult parseResult)
        {
            if (parseResult.Errors.Count > 0)
            {
                foreach (ParseError parseError in parseResult.Errors)
                {
                    Console.Error.WriteLine(parseError.Message);
                }
                return 1;
            }

            string docueyeAddress = parseResult.GetValue(CommandLineCommonOptions.DocueyeAddressOption)!;
            string adminToken = parseResult.GetValue(CommandLineCommonOptions.AdminTokenOption)!;
            string? elementDslId = parseResult.GetValue(this.openApiImportElementDslIdOption);
            string? elementId = parseResult.GetValue(this.openApiImportElementIdOption);
            string file = parseResult.GetValue(this.openApiImportFileOption)!;
            string workspaceId = parseResult.GetValue(this.openApiImportWorkspaceIdOption)!;

            var host = new CliHostBuilder().Build(new CliHostOptions(docueyeAddress, adminToken));

            var compatibilityCheckService = host.Services.GetRequiredService<ICompatibilityCheckService>();
            var isCompatible = await compatibilityCheckService.CheckCompatibility();
            if (!isCompatible)
            {
                return 1;
            }

            var importOpenApiFileService = host.Services.GetRequiredService<IImportOpenApiFileService>();

            var result = await importOpenApiFileService.Import(new ImportOpenApiFileParameters() { 
                ElementDslId = elementDslId,
                ElementId = elementId,
                OpenApiFile = file,
                WorkspaceId = workspaceId
            });

            if (!result)
            {
                return 1;
            }

            return 0;
        }
    }
}
