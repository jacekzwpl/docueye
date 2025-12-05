using DocuEye.CLI.Application.Services.Compatibility;
using DocuEye.CLI.Application.Services.DeleteWorkspace;
using DocuEye.CLI.Application.Services.DSL;
using DocuEye.CLI.Commands;
using DocuEye.CLI.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.CLI
{
    public class WorkspaceDeleteCommand : Command
    {
        Option<string> workspaceDeleteWorkspaceIdOption;
        public WorkspaceDeleteCommand() : base("delete", "Deletes a workspace from DocuEye.")
        {

            workspaceDeleteWorkspaceIdOption = new("--id", "-i")
            {
                Description = "The ID of the Workspace to be deleted.",
                Required = true
            };


            this.Options.Add(CommandLineCommonOptions.DocueyeAddressOption);
            this.Options.Add(CommandLineCommonOptions.AdminTokenOption);
            this.Options.Add(workspaceDeleteWorkspaceIdOption);
            this.SetAction(async parseResult => await this.Run(parseResult));
        }
        public async Task<int> Run(ParseResult parseResult)
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
            string workspaceId = parseResult.GetValue(workspaceDeleteWorkspaceIdOption)!;
            var host = new CliHostBuilder().Build(new CliHostOptions(docueyeAddress, adminToken));

            var compatibilityCheckService = host.Services.GetRequiredService<ICompatibilityCheckService>();
            var isCompatible = await compatibilityCheckService.CheckCompatibility();
            if (!isCompatible)
            {
                return 1;
            }

            var deleteWorkspaceService = host.Services.GetRequiredService<IDeleteWorkspaceService>();
            await deleteWorkspaceService.DeleteWorkspace(workspaceId);
            return 0;

        }
    }
}
