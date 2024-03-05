using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Model
{
    public class StructurizrWorkspaceConfiguration
    {
        public string Visibility { get; set; } = "private";
        public IEnumerable<StructurizrWorkspaceConfigurationUser>? Users { get; set; }
    }
}
