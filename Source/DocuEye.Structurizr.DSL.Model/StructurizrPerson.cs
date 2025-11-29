using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.DSL.Model
{
    public class StructurizrPerson
    {
        public string Identifier { get; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Url { get; set; }
        public List<string>? Tags { get; set; }
        public string? GroupId { get; set; }
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        public IEnumerable<StructurizrPerspective>? Perspectives { get; set; }

        public StructurizrPerson(string identifier)
        {
            Identifier = identifier;
        }

        public StructurizrPerson(string identifier, string[] tags)
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
                Type = StructurizrModelElementType.Person,
                Properties = this.Properties,
                Perspectives = this.Perspectives,
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
            this.GroupId = element.GroupId;
        }

    }
}
