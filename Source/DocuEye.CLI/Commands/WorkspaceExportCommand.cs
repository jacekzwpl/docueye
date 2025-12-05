using DocuEye.CLI.Application.Services.DSL;
using DocuEye.CLI.Hosting;
using DocuEye.Structurizr.DslToJson;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace DocuEye.CLI.Commands
{
    public class WorkspaceExportCommand : Command
    {
        Option<FileInfo> workspaceExportFileOption;
        Option<string> exportFormatOption;


        public WorkspaceExportCommand() : base("export", "Exports a workspace to given format.")
        {
            this.workspaceExportFileOption = new("--file", "-f")
            {
                Description = "Path to workspace file.",
                Required = true
            };
            this.exportFormatOption = new("--format", "-t")
            {
                Description = "Specifies export format 'json' for export to json file.",
                Required = false,
                DefaultValueFactory = parseResult => "json",
            };
            this.exportFormatOption.AcceptOnlyFromAmong("json");

            this.Options.Add(this.workspaceExportFileOption);
            this.Options.Add(this.exportFormatOption);

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

            FileInfo worspaceFile = parseResult.GetValue(this.workspaceExportFileOption)!;
            string exportFormat = parseResult.GetValue(this.exportFormatOption)!;

            var host = new CliHostBuilder().Build(new CliHostOptions("https://docueye.com", "none"));
            var workspaceParser = host.Services.GetRequiredService<IWorkspaceParserService>();
            var workspace = workspaceParser.Parse(worspaceFile);
            if (workspace == null)
            {
                return 1;
            }

            var converter = new WorkspaceConverter(workspace, worspaceFile.DirectoryName);
            converter.Convert(Path.Combine(worspaceFile.DirectoryName, "workspace.json"));


            return 0;

        } 
     }
}
