using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.DSL.Model
{
    public class StructurizrRelationship
    {
        public string ModelId { get; set; } = null!;
        public string Identifier { get; }
        public string SourceIdentifier { get; set; } = null!;
        public string DestinationIdentifier { get; set; } = null!;
        public string? Description { get; set; }
        public string? Technology { get; set; }
        public string? Url { get; set; }
        public List<string> Tags { get; set; }
        public string LinkedRelationshipIdentifier { get; set; } = null!;
        public string LinkedRelationshipModelId { get; set; } = null!;
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        public IEnumerable<StructurizrPerspective>? Perspectives { get; set; }
        public StructurizrRelationship(string identifier, string[] tags)
        {
            Identifier = identifier;
            Tags = new List<string>(tags);
        }

        public StructurizrModelElement ToModelElement(string modelId = "")
        {
            return new StructurizrModelElement(modelId, this.Identifier)
            {
                Description = this.Description,
                Url = this.Url,
                Tags = this.Tags,
                Technology = this.Technology,
                Type = StructurizrModelElementType.Relationship,
                Properties = this.Properties,
                Perspectives = this.Perspectives
            };
        }

        public void FromModelElement(StructurizrModelElement element)
        {
            this.Description = element.Description;
            this.Url = element.Url;
            this.Technology = element.Technology;
            this.Tags = element.Tags ?? new List<string>();
            this.Properties = element.Properties;
            this.Perspectives = element.Perspectives;
        }

    }
}
