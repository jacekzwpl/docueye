using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.DSL.Model
{
    public class StructurizrGroup
    {
        public string Id { get; }
        public string Name { get; set; } = null!;
        public string? ParentId { get; set; }
        public StructurizrGroup(string id, string name, string? parentId)
        {
            Id = id;
            Name = name;
            ParentId = parentId;
        }
    }
}
