using DocuEye.CLI.Commands;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.CLI
{
    public class WorkspaceDeleteCommand : Command
    {
        public WorkspaceDeleteCommand() : base("delete", "TODO opis")
        {

            Option<string> workspaceDeleteWorkspaceIdOption = new("--id", "-i")
            {
                Description = "The ID of the Workspace to be deleted.",
                Required = true
            };


            this.Options.Add(CommandLineCommonOptions.DocueyeAddressOption);
            this.Options.Add(CommandLineCommonOptions.AdminTokenOption);
            this.Options.Add(workspaceDeleteWorkspaceIdOption);
            this.SetAction(parseResult => this.Run(parseResult));
        }
        public void Run(ParseResult parseResult)
        {
        }
    }
}
