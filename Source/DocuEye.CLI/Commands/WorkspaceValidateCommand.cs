using DocuEye.CLI.Application.Services.DSL;
using DocuEye.CLI.Hosting;
using DocuEye.Linter;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.CommandLine.Parsing;

namespace DocuEye.CLI.Commands
{
    public class WorkspaceValidateCommand : Command
    {
        Option<FileInfo> workspaceImportFileOption;

        public WorkspaceValidateCommand() : base("validate", "Validates dsl workspace.")
        {
            this.workspaceImportFileOption = new("--file", "-f")
            {
                Description = "Path to workspace DSL file.",
                Required = true
            };
            this.Options.Add(this.workspaceImportFileOption);
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

            var host = new CliHostBuilder().Build(new CliHostOptions("https://docueye.com", "none"));

            var workspaceParser = host.Services.GetRequiredService<IWorkspaceParserService>();
            var workspace = workspaceParser.Parse(worspaceFile);
            if (workspace == null)
            {
                Console.Error.WriteLine("Workspace validation failed.");
                return 1;
            }

            var linter = new ArchitectureLinter(new Linter.Model.LinterModel()
            {
                Elements = Enumerable.Empty<Linter.Model.LinterModelElement>(),
                Relationships = Enumerable.Empty<Linter.Model.LinterModelRelationship>()
            }, host.Services.GetRequiredService<ILogger<ArchitectureLinter>>());
            linter.LoadConfigurationFromFile("C:\\nCode\\docueye\\Source\\DocuEye.Linter.Poc\\rules.json").GetAwaiter().GetResult();
            
            if (!linter.Analyze())
            {
                Console.Error.WriteLine("Workspace is invalid.");
                return 1;
            }

            Console.WriteLine("Workspace is valid.");
            return 0;
        }
    }
}
