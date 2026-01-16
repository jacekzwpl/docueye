using DocuEye.Linter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocuEye.Structurizr.DSL.Model.Maps
{
    public static class StructurizrRelationshipMap
    {
        public static LinterModelRelationship ToLinterModelRelationship(this StructurizrRelationship relationship, IEnumerable<StructurizrModelElement> elements) { 
            return new LinterModelRelationship
            {
                Source = elements.Where(o => o.Identifier == relationship.SourceIdentifier).First().ToLinterModelElement(),
                Destination = elements.Where(o => o.Identifier == relationship.DestinationIdentifier).First().ToLinterModelElement(),
                Technology = relationship.Technology ?? string.Empty,
                Description = relationship.Description ?? string.Empty,
                Properties = new Dictionary<string, string>(relationship.Properties),
                Tags = new List<string>(relationship.Tags)
            };
        }

        public static IEnumerable<LinterModelRelationship> ToLinterModelRelationships(this IEnumerable<StructurizrRelationship> relationships, IEnumerable<StructurizrModelElement> elements) {
            var result = new List<LinterModelRelationship>();
            foreach (var relationship in relationships) {
                var linterRelationship =  relationship.ToLinterModelRelationship(elements);
                result.Add(linterRelationship);
            }
            return result.ToArray();
        }
    }
}
