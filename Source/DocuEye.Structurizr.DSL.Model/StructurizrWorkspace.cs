using System.Collections.Generic;

namespace DocuEye.Structurizr.DSL.Model
{
    public class StructurizrWorkspace
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        public string Identifiers { get; set; } = "flat";
        public string? Docs { get; set; }
        public string? Adrs { get; set; }
        public StructurizrModel Model { get; set; }
        public StructurizrViews Views { get; set; } 
        public StructurizrConfiguration? Configuration { get; set; }

        public StructurizrWorkspace()
        {
            Model = new StructurizrModel();
            Views = new StructurizrViews();
        }
    }
}
