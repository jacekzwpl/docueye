using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocuEye.Structurizr.Json.Model.Maps
{
    public static class StructurizrJsonSoftwareSystemInstanceMap
    {
        public static ElementToImport ConvertToApModel(this StructurizrJsonSoftwareSystemInstance source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new ElementToImport
            {
                StructurizrId = source.Id ?? string.Empty,
                StructurizrInstanceOfId = source.SoftwareSystemId,
                Type = ElementType.SoftwareSystemInstance,
                Tags = string.IsNullOrWhiteSpace(source.Tags) ? null : source.Tags.Split(",").ToArray(),
                Properties = source.Properties ?? new Dictionary<string, string>(),
                DslId = source.DslId
            };
        }
    }
}
