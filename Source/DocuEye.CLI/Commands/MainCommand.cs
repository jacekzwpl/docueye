using System.CommandLine;

namespace DocuEye.CLI.Commands
{
    public class MainCommand : RootCommand
    {
        public MainCommand(string description = "DocuEye CLI - A command line interface for DocuEye.") : base(description)
        {
            // Create workspace command
            var workspaceCommand = new WorkspaceCommand();
            // Create openapi command
            var openapiCommand = new OpenapiCommand();
            var docFileCommand = new DocFileCommand();

            // Add subcommands to root command
            this.Subcommands.Add(workspaceCommand);
            this.Subcommands.Add(openapiCommand);
            this.Subcommands.Add(docFileCommand);
        }
    }
}
