using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.DSL.Model
{
    public class StructurizrStyles
    {
        public List<StructurizrRelationshipStyle> RelationshipStyles { get; set; } = new List<StructurizrRelationshipStyle>();
        public List<StructurizrElementStyle> ElementStyles { get; set; } = new List<StructurizrElementStyle>();
    }
}
