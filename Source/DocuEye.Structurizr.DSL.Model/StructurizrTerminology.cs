using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.DSL.Model
{
    public class StructurizrTerminology
    {
        public string? Person { get; set; } 
        public string? SoftwareSystem { get; set; }
        public string? Container { get; set; }
        public string? Component { get; set; }
        public string? Relationship { get; set; } 
        public string? DeploymentNode { get; set; } 
        public string? InfrastructureNode { get; set; } 
    }
}
