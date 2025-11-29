using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.DSL.Model
{
    public class StructurizrConfiguration
    {
        public string? Scope { get; set; }
        public string? Visibility { get; set; }
        public IEnumerable<StructurizrConfigurationUser>? Users { get; set; }

    }
}
