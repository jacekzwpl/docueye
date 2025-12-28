using DocuEye.CLI.Application.Services.DSL;
using DocuEye.CLI.Hosting;
using DocuEye.Linter;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.CommandLine.Parsing;
using DocuEye.Structurizr.DSL.Model.Maps;

namespace DocuEye.CLI.Commands
{
    public class WorkspaceValidateCommand : Command
    {
        Option<FileInfo> workspaceImportFileOption;
        Option<string> linterConfiguration;

        public WorkspaceValidateCommand() : base("validate", "Validates dsl workspace.")
        {
            this.workspaceImportFileOption = new("--file", "-f")
            {
                Description = "Path to workspace DSL file.",
                Required = true
            };

            this.linterConfiguration = new Option<string>("--linter-config", "-lc")
            {
                Description = "Path or Url to linter configuration file.",
                Required = false
            };
            this.Options.Add(this.workspaceImportFileOption);
            this.Options.Add(this.linterConfiguration);
            this.SetAction(async parseResult => await this.Run(parseResult));
        }

        public async Task<int> Run(ParseResult parseResult, CancellationToken cancellationToken = default)
        {
            if (parseResult.Errors.Count > 0)
            {
                foreach (ParseError parseError in parseResult.Errors)
                {
                    Console.Error.WriteLine(parseError.Message);
                }
                return 1;
            }

            FileInfo worspaceFile = parseResult.GetValue(this.workspaceImportFileOption)!;
            string? linterConfig = parseResult.GetValue(this.linterConfiguration);


            var host = new CliHostBuilder().Build(new CliHostOptions("https://docueye.com", "none"));

            var workspaceParser = host.Services.GetRequiredService<IWorkspaceParserService>();
            var workspace = workspaceParser.Parse(worspaceFile);
            if (workspace == null)
            {
                Console.Error.WriteLine("Workspace validation failed.");
                return 1;
            }

            if(linterConfig != null)
            {
                var linter = new ArchitectureLinter(new Linter.Model.LinterModel()
                {
                    Elements = workspace.Model.Elements.ToLinterModelElements(),
                    Relationships = workspace.Model.Relationships.ToLinterModelRelationships(workspace.Model.Elements),
                }, host.Services.GetRequiredService<ILogger<ArchitectureLinter>>());
                linter.LoadConfigurationFromFile(linterConfig).GetAwaiter().GetResult();

                if (!linter.Analyze())
                {
                    Console.Error.WriteLine("Workspace is invalid.");
                    return 1;
                }
            }

            Console.WriteLine("Workspace is valid.");
            return 0;
        }
    }
}
