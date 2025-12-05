using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.CLI.Commands
{
    public class OpenapiImportCommand : Command
    {
        public OpenapiImportCommand() : base("import", "Imports or updates OpenAPI specification for given element.")
        {

            Option<string> openApiImportFileOption = new("--file", "-f")
            {
                Description = "Path to openapi specification file for element.",
                Required = true
            };

            Option<string> openApiImportElementIdOption = new("--element-id", "-e")
            {
                Description = "The ID of element for which this import is created. Required only if --element-dsl-id option is not set.",
            };

            Option<string> openApiImportElementDslIdOption = new("--element-dsl-id", "-d")
            {
                Description = "The DSL ID of element for which this import is created. Required only if --element-id option is not set.",
            };

            Option<string> openApiImportWorkspaceIdOption = new("--id", "-i")
            {
                Description = "The ID of the Workspace where element exists.",
                Required = true
            };

            this.Options.Add(CommandLineCommonOptions.DocueyeAddressOption);
            this.Options.Add(CommandLineCommonOptions.AdminTokenOption);
            this.Options.Add(openApiImportFileOption);
            this.Options.Add(openApiImportWorkspaceIdOption);
            this.Options.Add(openApiImportElementIdOption);
            this.Options.Add(openApiImportElementDslIdOption);
            this.SetAction(parseResult => this.Run(parseResult));
        }

        private void Run(ParseResult parseResult)
        {
            throw new NotImplementedException();
        }
    }
}
