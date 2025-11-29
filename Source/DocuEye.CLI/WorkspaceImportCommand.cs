using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.CLI
{
    public class WorkspaceImportCommand : Command
    {
        public WorkspaceImportCommand(CommandLineCommonOptions commonOptions) : base("import", "TODO:OPIS") {

            Option<string> workspaceImportModeOption = new("--mode", "-m")
            {
                Description = "Specifies import mode 'dsl' for import from dsl file, 'json; for import from json file.",
                Required = true,
                DefaultValueFactory = parseResult => "dsl",
            };
            workspaceImportModeOption.AcceptOnlyFromAmong("dsl", "json");

            Option<string> workspaceImportFileOption = new("--file", "-f")
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
            this.Options.Add(workspaceImportFileOption);
            this.Options.Add(workspaceImportIdOption);
            this.Options.Add(workspaceImportKeyOption);
            this.Options.Add(workspaceImportSourceLinkOption);

            this.SetAction(parseResult => this.Run(parseResult));
        }

        public void Run(ParseResult parseResult)
        {

        } 
    }
}
