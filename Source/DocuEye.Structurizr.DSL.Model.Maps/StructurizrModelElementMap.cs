using DocuEye.Linter.Model;
using System;
using System.Collections.Generic;

namespace DocuEye.Structurizr.DSL.Model.Maps
{
    public static class StructurizrModelElementMap
    {
        public static LinterModelElement ToLinterModelElement(this StructurizrModelElement element) { 
            return new LinterModelElement
            {
                Identifier = element.Identifier,
                ParentIdentifier = element.ParentIdentifier,
                Name = element.Name,
                Tags = new List<string>(element.Tags ?? new List<string>()).ToArray() ?? Array.Empty<string>(),
                Technology = element.Technology ?? string.Empty,
                Description = element.Description ?? string.Empty,
                InstanceOfIdentifier = element.InstanceOfIdentifier,
                Properties = new Dictionary<string, string>(element.Properties)
            };

        }

        public static IEnumerable<LinterModelElement> ToLinterModelElements(this IEnumerable<StructurizrModelElement> elements) {

            var result = new List<LinterModelElement>();
            foreach (var element in elements) {
                var linterElement =  element.ToLinterModelElement();
                result.Add(linterElement);
            }
            return result.ToArray();
        }
    }
}
