using DocuEye.CLI.Application.Services.Compatibility;
using DocuEye.CLI.Application.Services.DeleteOpenApiFile;
using DocuEye.CLI.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;
using System.CommandLine.Parsing;

namespace DocuEye.CLI.Commands
{
    public class OpenapiDeleteCommand : Command
    {
        private Option<string> openApiImportElementIdOption;
        private Option<string> openApiImportElementDslIdOption;
        private Option<string> openApiImportWorkspaceIdOption;

        public OpenapiDeleteCommand() : base("delete", "Delete OpenAPI file for element")
        {
            this.openApiImportElementIdOption = new("--element-id", "-e")
            {
                Description = "The ID of element for which this operation is created. Required only if --element-dsl-id option is not set.",
            };

            this.openApiImportElementDslIdOption = new("--element-dsl-id", "-d")
            {
                Description = "The DSL ID of element for which this operation is created. Required only if --element-id option is not set.",
            };

            this.openApiImportWorkspaceIdOption = new("--id", "-i")
            {
                Description = "The ID of the Workspace where element exists.",
                Required = true
            };
            this.Options.Add(CommandLineCommonOptions.DocueyeAddressOption);
            this.Options.Add(CommandLineCommonOptions.AdminTokenOption);
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
            string workspaceId = parseResult.GetValue(this.openApiImportWorkspaceIdOption)!;

            var host = new CliHostBuilder().Build(new CliHostOptions(docueyeAddress, adminToken));

            var compatibilityCheckService = host.Services.GetRequiredService<ICompatibilityCheckService>();
            var isCompatible = await compatibilityCheckService.CheckCompatibility();
            if (!isCompatible)
            {
                return 1;
            }

            var deleteOpenApiFileService = host.Services.GetRequiredService<IDeleteOpenApiFileService>();

            var result = await deleteOpenApiFileService.DeleteOpenApiFile(workspaceId, elementId, elementDslId);

            return result ? 0 : 1;
        }

    }
}
