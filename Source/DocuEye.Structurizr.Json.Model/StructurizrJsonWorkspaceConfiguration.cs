using System.Collections.Generic;

namespace DocuEye.Structurizr.Json.Model
{
    public class StructurizrJsonWorkspaceConfiguration
    {
        public string Visibility { get; set; } = "public";
        public IEnumerable<StructurizrJsonWorkspaceConfigurationUser>? Users { get; set; }
    }
}
