using DocuEye.Linter.Model;
using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocuEye.Structurizr.Json.Model.Maps
{
    public static class StructurizrJsonContainerInstanceMap
    {
        public static ElementToImport ConvertToApModel(this StructurizrJsonContainerInstance source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new ElementToImport
            {
                StructurizrId = source.Id ?? string.Empty,
                StructurizrInstanceOfId = source.ContainerId,
                Type = ElementType.ContainerInstance,
                Tags = string.IsNullOrWhiteSpace(source.Tags) ? null : source.Tags.Split(",").ToArray(),
                Properties = source.Properties ?? new Dictionary<string, string>(),
                DslId = source.DslId
            };
        }

        public static LinterModelElement ToLinterModelElement(this StructurizrJsonContainerInstance source, string parentIdentifier, LinterModelElement instanceOfElement)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new LinterModelElement()
            {
                Identifier = source.DslId,
                Tags = string.IsNullOrWhiteSpace(source.Tags) ? new List<string>() : source.Tags.Split(",").ToList(),
                Properties = source.Properties != null ? new Dictionary<string, string>(source.Properties) : new Dictionary<string, string>(),
                ParentIdentifier = parentIdentifier,
                InstanceOfIdentifier = instanceOfElement.Identifier,
                Name = instanceOfElement.Name ?? string.Empty,
                Description = instanceOfElement.Description,
                Technology = instanceOfElement.Technology
            };
        }
    }
}
