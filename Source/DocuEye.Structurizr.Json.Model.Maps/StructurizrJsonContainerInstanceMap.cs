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
    }
}
