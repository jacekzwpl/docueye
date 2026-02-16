using DocuEye.CLI.Application.Services.DSL;
using DocuEye.CLI.Hosting;
using DocuEye.Linter;
using DocuEye.Structurizr.DSL.Model.Maps;
using DocuEye.Structurizr.DslToJson;
using DocuEye.Structurizr.Json.Model.Maps;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.CommandLine.Parsing;

namespace DocuEye.CLI.Commands
{
    public class WorkspaceValidateCommand : Command
    {
        Option<FileInfo> workspaceImportFileOption;
        Option<string> linterConfiguration;
        Option<string> workspaceValidateModeOption;

        public WorkspaceValidateCommand() : base("validate", "Validates dsl workspace.")
        {
            this.workspaceImportFileOption = new("--file", "-f")
            {
                Description = "Path to workspace DSL file.",
                Required = true
            };

            this.workspaceValidateModeOption = new("--mode", "-m")
            {
                Description = "Specifies validation mode 'dsl' for validation dsl file, 'json; for validation json file.",
                Required = true,
                DefaultValueFactory = parseResult => "dsl",
            };
            this.workspaceValidateModeOption.AcceptOnlyFromAmong("dsl", "json");

            this.linterConfiguration = new Option<string>("--linter-config", "-lc")
            {
                Description = "Path or Url to linter configuration file.",
                Required = false
            };
            this.Options.Add(this.workspaceImportFileOption);
            this.Options.Add(this.linterConfiguration);
            this.Options.Add(this.workspaceValidateModeOption);
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
            string validationMode = parseResult.GetValue<string>(this.workspaceValidateModeOption)!;

            var host = new CliHostBuilder().Build(new CliHostOptions("https://docueye.com", "none"));

            if (validationMode == "dsl")
            {
                var workspaceParser = host.Services.GetRequiredService<IWorkspaceParserService>();
                var workspace = workspaceParser.Parse(worspaceFile);
                if (workspace == null)
                {
                    Console.Error.WriteLine("Workspace validation failed.");
                    return 1;
                }

                if (linterConfig != null)
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
                
            }
            else if (validationMode == "json")
            {
                var jsonText = File.ReadAllText(worspaceFile.FullName);
                var jsonWorkspace = new WorkspaceSerializer().Deserialize(jsonText); //JsonSerializer.Deserialize<StructurizrJsonWorkspace>(jsonText, this.serializerOptions);

                if (linterConfig != null && jsonWorkspace.Model != null)
                {
                    var elements = jsonWorkspace.Model.ToLinterModelElements();
                    var relationships = jsonWorkspace.Model.ToLinterModelRelationships(elements);

                    var linter = new ArchitectureLinter(new Linter.Model.LinterModel()
                    {
                        Elements = elements,
                        Relationships = relationships,
                    }, host.Services.GetRequiredService<ILogger<ArchitectureLinter>>());
                    linter.LoadConfigurationFromFile(linterConfig).GetAwaiter().GetResult();

                    if (!linter.Analyze())
                    {
                        Console.Error.WriteLine("Workspace is invalid.");
                        return 1;
                    }
                }
            }

            Console.WriteLine("Workspace is valid.");
            return 0;
        }
    }
}
