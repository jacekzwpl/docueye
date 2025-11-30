using DocuEye.CLI.Application.Services.DSL;
using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;
using System.CommandLine.Parsing;

namespace DocuEye.CLI
{
    public class WorkspaceImportCommand : Command
    {
        Option<FileInfo> workspaceImportFileOption;
        CommandLineCommonOptions commonOptions;

        public WorkspaceImportCommand(CommandLineCommonOptions commonOptions) : base("import", "TODO:OPIS") {

            this.commonOptions = commonOptions;
            Option<string> workspaceImportModeOption = new("--mode", "-m")
            {
                Description = "Specifies import mode 'dsl' for import from dsl file, 'json; for import from json file.",
                Required = true,
                DefaultValueFactory = parseResult => "dsl",
            };
            workspaceImportModeOption.AcceptOnlyFromAmong("dsl", "json");

            this.workspaceImportFileOption = new("--file", "-f")
            {
                Description = "Path to workspace file. Depending on mode option should be path to dsl file or json file.",
                Required = true
            };

            Option<string> workspaceImportIdOption = new("--id", "-i")
            {
                Description = "The ID of the Workspace. If not provided the new workspace will be created. Also if workspace with given id does not exists new workspace will be created."
            };

            Option<string> workspaceImportKeyOption = new("--key", "-k")
            {
                Description = "Unique import key. If not provided, one will be generated."
            };

            Option<string> workspaceImportSourceLinkOption = new("--source-link", "-s")
            {
                Description = "Link to source version from which workspace is imported ex. link to PR or commit on github."
            };



            this.Options.Add(commonOptions.DocueyeAddressOption);
            this.Options.Add(commonOptions.AdminTokenOption);
            this.Options.Add(workspaceImportModeOption);
            this.Options.Add(this.workspaceImportFileOption);
            this.Options.Add(workspaceImportIdOption);
            this.Options.Add(workspaceImportKeyOption);
            this.Options.Add(workspaceImportSourceLinkOption);

            this.SetAction(parseResult => this.Run(parseResult));
        }

        public Task<int> Run(ParseResult parseResult, CancellationToken cancellationToken = default)
        {
            //if (parseResult.Errors.Count == 0 && parseResult.GetValue(this.commonOptions.DocueyeAddressOption) is string docueyeAddress)
            //{
            //    //ReadFile(parsedFile);
            //    return Task.FromResult(0);
            //}

            var host = new CliHostBuilder().Build(new CliHostOptions("https://docueye.com", "token"));
            var workspaceParser = host.Services.GetRequiredService<IWorkspaceParserService>();

            if (parseResult.Errors.Count == 0 && parseResult.GetValue(this.workspaceImportFileOption) is FileInfo parsedFile)
            {
                var workspace = workspaceParser.Parse(parsedFile);
                if (workspace != null) {
                    return Task.FromResult(0);
                }
                return Task.FromResult(1);
            }
        
            foreach (ParseError parseError in parseResult.Errors)
            {
                Console.Error.WriteLine(parseError.Message);
            }
            return Task.FromResult(1);
        } 
    }
}
