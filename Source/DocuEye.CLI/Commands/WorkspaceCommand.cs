using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.CLI.Commands
{
    public class WorkspaceCommand : Command
    {
        public WorkspaceCommand() : base("workspace", "Commands for managing workspaces")
        {
            this.Subcommands.Add(new WorkspaceImportCommand());
            this.Subcommands.Add(new WorkspaceDeleteCommand());
            this.Subcommands.Add(new WorkspaceExportCommand());
            this.Subcommands.Add(new WorkspaceValidateCommand());
        }
    }
}
