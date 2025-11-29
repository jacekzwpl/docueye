using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DocuEye.Structurizr.DSL.Model
{
    public class StructurizrComponent
    {
        public string Identifier { get; }
        public string ContainerIdentifier { get; private set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Url { get; set; }
        public string? Docs { get; set; }
        public string? Adrs { get; set; }
        public string? GroupId { get; set; }
        public string? Technology { get; set; }
        public List<string> Tags { get; set; } = null!;
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        public IEnumerable<StructurizrPerspective>? Perspectives { get; set; }

        public StructurizrComponent(string identifier, string containerIdentifier)
        {
            Identifier = identifier;
            Tags = new List<string>();
            ContainerIdentifier = containerIdentifier;
        }

        public StructurizrComponent(string identifier, string containerIdentifier, string[] tags)
        {
            Identifier = identifier;
            Tags = new List<string>(tags);
            ContainerIdentifier = containerIdentifier;
        }

        public StructurizrModelElement ToModelElement(string modelId = "")
        {
            return new StructurizrModelElement(modelId, this.Identifier)
            {
                Name = this.Name,
                Description = this.Description,
                Url = this.Url,
                Tags = this.Tags,
                Type = StructurizrModelElementType.Component,
                Properties = this.Properties,
                Perspectives = this.Perspectives,
                GroupId = this.GroupId,
                Technology = this.Technology,
                ParentIdentifier = this.ContainerIdentifier
            };
        }

        public void FromModelElement(StructurizrModelElement element)
        {
            this.Name = element.Name;
            this.Description = element.Description;
            this.Url = element.Url;
            this.Tags = element.Tags ?? new List<string>();
            this.Properties = element.Properties;
            this.Perspectives = element.Perspectives;
            this.GroupId = element.GroupId;
            this.Technology = element.Technology;
            this.ContainerIdentifier = element.ParentIdentifier ?? string.Empty;
        }
    }
}
