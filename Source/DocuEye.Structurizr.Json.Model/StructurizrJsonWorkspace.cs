using System;
using System.Collections.Generic;

namespace DocuEye.Structurizr.Json.Model
{
    public class StructurizrJsonWorkspace
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IDictionary<string,string> Properties { get; set; } = new Dictionary<string,string>();
        public StructurizrJsonModel? Model { get; set; }
        public StructurizrJsonViews? Views { get; set; }
        public StructurizrJsonDocumentation? Documentation { get; set; }
        public StructurizrJsonWorkspaceConfiguration? Configuration { get; set; }
    }
}
