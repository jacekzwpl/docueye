using System;
using System.Collections.Generic;

namespace DocuEye.Structurizr.Model
{
    public class StructurizrWorkspace
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IDictionary<string,string> Properties { get; set; } = new Dictionary<string,string>();
        public StructurizrModel? Model { get; set; }
        public StructurizrViews? Views { get; set; }
        public StructurizrDocumentation? Documentation { get; set; }
    }
}
