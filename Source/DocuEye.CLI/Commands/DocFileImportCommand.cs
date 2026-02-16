using DocuEye.CLI.Application.Services.Compatibility;
using DocuEye.CLI.Application.Services.ImportDocumentationFile;
using DocuEye.CLI.Application.Services.ImportOpenApiFile;
using DocuEye.CLI.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;
using System.CommandLine.Parsing;

namespace DocuEye.CLI.Commands
{
    public class DocFileImportCommand : Command
    {
        private Option<string> docFileImportFileOption;
        private Option<string> docFileImportElementIdOption;
        private Option<string> docFileImportElementDslIdOption;
        private Option<string> docFileImportWorkspaceIdOption;
        Option<string> docFileTypeOption;

        public DocFileImportCommand() : base("import", "Imports or updates documentation file for given element.")
        {
            this.docFileImportFileOption = new("--file", "-f")
            {
                Description = "Path to openapi specification file for element.",
                Required = true
            };

            this.docFileImportElementIdOption = new("--element-id", "-e")
            {
                Description = "The ID of element for which this import is created. Required only if --element-dsl-id option is not set.",
            };

            this.docFileImportElementDslIdOption = new("--element-dsl-id", "-d")
            {
                Description = "The DSL ID of element for which this import is created. Required only if --element-id option is not set.",
            };

            this.docFileImportWorkspaceIdOption = new("--id", "-i")
            {
                Description = "The ID of the Workspace where element exists.",
                Required = true
            };

            this.docFileTypeOption = new("--type", "-t")
            {
                Description = "Specifies import mode 'dsl' for import from dsl file, 'json; for import from json file.",
                Required = true
            };
            this.docFileTypeOption.AcceptOnlyFromAmong("openapi", "asyncapi");

            this.Options.Add(CommandLineCommonOptions.DocueyeAddressOption);
            this.Options.Add(CommandLineCommonOptions.AdminTokenOption);
            this.Options.Add(this.docFileImportFileOption);
            this.Options.Add(this.docFileImportElementIdOption);
            this.Options.Add(this.docFileImportElementDslIdOption);
            this.Options.Add(this.docFileImportWorkspaceIdOption);
            this.Options.Add(this.docFileTypeOption);
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
            string? elementDslId = parseResult.GetValue(this.docFileImportElementDslIdOption);
            string? elementId = parseResult.GetValue(this.docFileImportElementIdOption);
            string file = parseResult.GetValue(this.docFileImportFileOption)!;
            string workspaceId = parseResult.GetValue(this.docFileImportWorkspaceIdOption)!;
            string type = parseResult.GetValue(this.docFileTypeOption)!;

            var host = new CliHostBuilder().Build(new CliHostOptions(docueyeAddress, adminToken));

            var compatibilityCheckService = host.Services.GetRequiredService<ICompatibilityCheckService>();
            var isCompatible = await compatibilityCheckService.CheckCompatibility();
            if (!isCompatible)
            {
                return 1;
            }

            var importDocumentationFileService = host.Services.GetRequiredService<IImportDocumentationFileService>();

            var result = await importDocumentationFileService.Import(new ImportDocumentationFileParameters()
            {
                ElementDslId = elementDslId,
                ElementId = elementId,
                DocumentationFile = file,
                WorkspaceId = workspaceId,
                DocumentationFileType = type
            });

            if (!result)
            {
                return 1;
            }

            return 0;
        }
    }
}
