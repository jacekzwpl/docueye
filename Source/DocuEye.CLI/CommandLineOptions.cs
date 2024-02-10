﻿using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.CLI
{
    public class CommandLineOptions
    {
        [Option("import", Required = true, HelpText = "Import mode. 'workspace' for workspace import. 'openapifile' for import openapi specification for element.")]
        public string Import { get; set; } = null!;
        [Option("docueyeAddress", Required = true, HelpText = "DocuEye address ex. http://localhost:8080.")]
        public string DocueyeAddress { get; set; } = null!;
        [Option("adminToken", Required = true, HelpText = "The Admin token from DocuEye configuration.")]
        public string AdminToken { get; set; } = null!;
        [Option("workspaceFile", Required = false, HelpText = "Path to workspace.json file, generated by structuriz cli.")]
        public string? WorkspaceFile { get; set; }
        [Option("workspaceId", Required = false, HelpText = "The ID of the Workspace. If not provided the new workspace will be created. Also if workspace with given id does not exists new workspace will be created.")]
        public string? WorkspaceId { get; set; }
        [Option("importKey", Required = false, HelpText = "Unique import key. If not provided, one will be generated.")]
        public string? ImportKey { get; set; }
        [Option("sourceLink", Required = false, HelpText = "Link to source version from whitch workspace is imported ex. link to PR or commit on github.")]
        public string? SourceLink { get; set; }
        [Option("elementId", Required = false, HelpText = "The ID of element for witchi this import is created.")]
        public string? ElementId { get; set; }
        [Option("elementDslId", Required = false, HelpText = "The ID of element for witchi this import is created.")]
        public string? ElementDslId { get; set; }
        [Option("openApiFile", Required = false, HelpText = "Path to openapi specification file.")]
        public string? OpenApiFile { get; set; }


    }
}
