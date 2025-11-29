using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace DocuEye.Structurizr.DSL.Model
{
    public class StructurizrCustomElement
    {
        public string Identifier { get; }
        public string Name { get; set; } = null!;
        public string? Metadata { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public List<string>? Tags { get; set; }
        public string? GroupId { get; set; }
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        public IEnumerable<StructurizrPerspective>? Perspectives { get; set; }

        public StructurizrCustomElement(string identifier)
        {
            Identifier = identifier;
        }

        public StructurizrCustomElement(string identifier, string[] tags)
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
                Type = StructurizrModelElementType.CustomElement,
                Properties = this.Properties,
                Perspectives = this.Perspectives,
                Metadata = this.Metadata,
                GroupId = this.GroupId
            };
        }

        public void FromModelElement(StructurizrModelElement element)
        {
            this.Name = element.Name;
            this.Description = element.Description;
            this.Url = element.Url;
            this.Tags = element.Tags;
            this.Properties = element.Properties;
            this.Perspectives = element.Perspectives;
            this.Metadata = element.Metadata;
            this.GroupId = element.GroupId;
        }
    }
}
