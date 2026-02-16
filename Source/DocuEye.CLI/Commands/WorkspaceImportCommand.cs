using DocuEye.CLI.Application.Services.Compatibility;
using DocuEye.CLI.Application.Services.DSL;
using DocuEye.CLI.Application.Services.ImportWorkspace;
using DocuEye.CLI.Commands;
using DocuEye.CLI.Hosting;
using DocuEye.Linter;
using DocuEye.Structurizr.DSL.Model;
using DocuEye.Structurizr.DSL.Model.Maps;
using DocuEye.Structurizr.DslToJson;
using DocuEye.Structurizr.Json.Model;
using DocuEye.Structurizr.Json.Model.Maps;
using DocuEye.WorkspaceImporter.Api.Model.Issues;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.CommandLine.Parsing;

namespace DocuEye.CLI
{
    public class WorkspaceImportCommand : Command
    {
        Option<FileInfo> workspaceImportFileOption;
        Option<string> workspaceImportModeOption;
        Option<string> workspaceImportIdOption;
        Option<string> workspaceImportKeyOption;
        Option<string> workspaceImportSourceLinkOption;
        Option<string?> linterConfiguration;


        public WorkspaceImportCommand() : base("import", "Imports workspace to DocuEye.") {


            this.workspaceImportModeOption = new("--mode", "-m")
            {
                Description = "Specifies import mode 'dsl' for import from dsl file, 'json; for import from json file.",
                Required = true,
                DefaultValueFactory = parseResult => "dsl",
            };
            this.workspaceImportModeOption.AcceptOnlyFromAmong("dsl", "json");

            this.workspaceImportFileOption = new("--file", "-f")
            {
                Description = "Path to workspace file. Depending on mode option should be path to dsl file or json file.",
                Required = true
            };

            this.workspaceImportIdOption = new("--id", "-i")
            {
                Description = "The ID of the Workspace. If not provided the new workspace will be created. Also if workspace with given id does not exists new workspace will be created."
            };

            this.workspaceImportKeyOption = new("--key", "-k")
            {
                Description = "Unique import key. If not provided, one will be generated."
            };

            this.workspaceImportSourceLinkOption = new("--source-link", "-s")
            {
                Description = "Link to source version from which workspace is imported ex. link to PR or commit on github."
            };

            this.linterConfiguration = new Option<string?>("--linter-config", "-lc")
            {
                Description = "Path or Url to linter configuration file.",
                Required = false
            };


            this.Options.Add(CommandLineCommonOptions.DocueyeAddressOption);
            this.Options.Add(CommandLineCommonOptions.AdminTokenOption);
            this.Options.Add(workspaceImportModeOption);
            this.Options.Add(workspaceImportFileOption);
            this.Options.Add(workspaceImportIdOption);
            this.Options.Add(workspaceImportKeyOption);
            this.Options.Add(workspaceImportSourceLinkOption);
            this.Options.Add(this.linterConfiguration);

            this.SetAction(async parseResult => await this.Run(parseResult));
        }

        

        public async Task<int> Run(ParseResult parseResult, CancellationToken cancellationToken = default)
        {
            //Console.Error.WriteLine("test");
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
            FileInfo worspaceFile = parseResult.GetValue(this.workspaceImportFileOption)!;
            string importMode = parseResult.GetValue<string>(this.workspaceImportModeOption)!;
            string workspaceId = parseResult.GetValue<string>(this.workspaceImportIdOption)!;
            string importKey = parseResult.GetValue<string>(this.workspaceImportKeyOption)!;
            string sourceLink = parseResult.GetValue<string>(this.workspaceImportSourceLinkOption)!;
            string? linterConfig = parseResult.GetValue<string?>(this.linterConfiguration);


            importKey ??= Guid.NewGuid().ToString();


            var host = new CliHostBuilder().Build(new CliHostOptions(docueyeAddress, adminToken));

            var compatibilityCheckService = host.Services.GetRequiredService<ICompatibilityCheckService>();
            var isCompatible = await compatibilityCheckService.CheckCompatibility();
            if (!isCompatible)
            {
                return 1;
            }

            var importWorkspaceService = host.Services.GetRequiredService<IImportWorkspaceService>();
            if (importMode == "dsl")
            {
                var workspaceParser = host.Services.GetRequiredService<IWorkspaceParserService>();
                var workspace = workspaceParser.Parse(worspaceFile);
                if (workspace == null)
                {
                    return 1;
                }

                var issuesToImport = this.RunLinter(host, linterConfig, workspace: workspace);
                if (issuesToImport == null)
                {
                    return 1;
                }

                var converter = new WorkspaceConverter(workspace, worspaceFile.DirectoryName);
                var jsonWorkspace = converter.Convert();
                


                await importWorkspaceService.Import(
                    new ImportWorkspaceParameters(
                        importKey, jsonWorkspace, workspaceId, sourceLink, issuesToImport));
            }else if(importMode == "json")
            {
                var jsonText = File.ReadAllText(worspaceFile.FullName);
                var jsonWorkspace = new WorkspaceSerializer().Deserialize(jsonText); //JsonSerializer.Deserialize<StructurizrJsonWorkspace>(jsonText, this.serializerOptions);

                var issuesToImport = this.RunLinter(host, linterConfig, jsonWorkspace: jsonWorkspace);
                if (issuesToImport == null)
                {
                    return 1;
                }

                await importWorkspaceService.Import(
                    new ImportWorkspaceParameters(
                        importKey, jsonWorkspace, workspaceId, sourceLink, issuesToImport));
            }

            return 0;


        }

        private ArchitectureLinter CreateLinter(IHost host,  StructurizrWorkspace workspace)
        {
            return new ArchitectureLinter(new Linter.Model.LinterModel()
            {
                Elements = workspace.Model.Elements.ToLinterModelElements(),
                Relationships = workspace.Model.Relationships.ToLinterModelRelationships(workspace.Model.Elements),
            }, host.Services.GetRequiredService<ILogger<ArchitectureLinter>>());
        }

        private ArchitectureLinter CreateLinter(IHost host, StructurizrJsonWorkspace jsonWorkspace)
        {
            var elements = jsonWorkspace.Model?.ToLinterModelElements();
            var relationships = jsonWorkspace.Model?.ToLinterModelRelationships(elements);

            return new ArchitectureLinter(new Linter.Model.LinterModel()
            {
                Elements = elements ?? Enumerable.Empty<Linter.Model.LinterModelElement>(),
                Relationships = relationships ?? Enumerable.Empty<Linter.Model.LinterModelRelationship>(),
            }, host.Services.GetRequiredService<ILogger<ArchitectureLinter>>());
        }

        private IEnumerable<IssueToImport>? RunLinter(IHost host, string? linterConfig, StructurizrWorkspace? workspace = null, StructurizrJsonWorkspace? jsonWorkspace = null) {
            
            if (string.IsNullOrWhiteSpace(linterConfig)) 
            {  
                return Enumerable.Empty<IssueToImport>(); 
            }
            if (workspace == null && jsonWorkspace == null)
            {
                Console.Error.WriteLine("Workspace is invalid. Workspace is not provided for linter.");
                return null;
            }
            var linter = workspace != null ? 
                    this.CreateLinter(host, workspace) :
                    this.CreateLinter(host, jsonWorkspace!);
                linter.LoadConfigurationFromFile(linterConfig).GetAwaiter().GetResult();

            if (!linter.Analyze())
            {
                Console.Error.WriteLine("Workspace is invalid.");
                return null;
            }

            return linter.Issues.Select(i => new IssueToImport()
            {
                Element = i.Element != null ? new IssueElementToImport()
                {
                    Identifier = i.Element.Identifier,
                    Name = i.Element.Name,
                } : null,
                Relationship = i.Relationship != null ? new IssueRelationshipToImport()
                {
                    Identifier = i.Relationship.Identifier,
                    Source = new IssueElementToImport()
                    {
                        Identifier = i.Relationship.Source.Identifier,
                        Name = i.Relationship.Source.Name,
                    },
                    Destination = new IssueElementToImport()
                    {
                        Identifier = i.Relationship.Destination.Identifier,
                        Name = i.Relationship.Destination.Name,
                    }
                } : null,
                Message = i.Message,
                SeverityValue = i.SeverityValue,
                Rule = new IssueRuleToImport()
                {
                    Id = i.Rule.Id,
                    Name = i.Rule.Name,
                    Description = i.Rule.Description,
                    HelpLink = i.Rule.HelpLink
                }
            }).ToArray();
        }
    }
}
