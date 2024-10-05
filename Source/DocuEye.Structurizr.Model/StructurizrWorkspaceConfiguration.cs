using System.Collections.Generic;

namespace DocuEye.Structurizr.Model
{
    public class StructurizrWorkspaceConfiguration
    {
        public string Visibility { get; set; } = "public";
        public IEnumerable<StructurizrWorkspaceConfigurationUser>? Users { get; set; }
    }
}
