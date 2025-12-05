using System.CommandLine;

namespace DocuEye.CLI.Commands
{
    public class MainCommand : RootCommand
    {
        public MainCommand(string description = "DocuEye CLI - A command line interface for DocuEye.") : base(description)
        {
            // Create workspace command
            var workspaceCommand = new WorkspaceCommand();
            workspaceCommand.Subcommands.Add(new WorkspaceImportCommand());
            workspaceCommand.Subcommands.Add(new WorkspaceDeleteCommand());
            workspaceCommand.Subcommands.Add(new WorkspaceExportCommand());
            // Create openapi command
            var openapiCommand = new OpenapiCommand();
            openapiCommand.Subcommands.Add(new OpenapiImportCommand());
            // Add subcommands to root command
            this.Subcommands.Add(workspaceCommand);
            this.Subcommands.Add(openapiCommand);
        }
    }
}
