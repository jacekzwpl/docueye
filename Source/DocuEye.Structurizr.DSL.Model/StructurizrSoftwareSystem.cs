using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.DSL.Model
{
    public class StructurizrSoftwareSystem
    {
        public string Identifier { get; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Url { get; set; }
        public string? Docs { get; set; }
        public string? Adrs { get; set; }
        public string? GroupId { get; set; }
        public List<string> Tags { get; set; } = null!;
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        public IEnumerable<StructurizrPerspective>? Perspectives { get; set; }

        public StructurizrSoftwareSystem(string identifier)
        {
            Identifier = identifier;
            Tags = new List<string>();
        }

        public StructurizrSoftwareSystem(string identifier, string[] tags)
        {
            Identifier = identifier;
            Tags = new List<string>(tags);
        }

        public StructurizrModelElement ToModelElement(string modelId = "")
        {
            return new StructurizrModelElement(modelId, this.Identifier)
            {
                Name = this.Name,
                Description = this.Description,
                Url = this.Url,
                Tags = this.Tags,
                Type = StructurizrModelElementType.SoftwareSystem,
                Properties = this.Properties,
                Perspectives = this.Perspectives,
                Adrs = this.Adrs,
                Docs = this.Docs,
                GroupId = this.GroupId
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
            this.Adrs = element.Adrs;
            this.Docs = element.Docs;
            this.GroupId = element.GroupId;
        }
    }
}
