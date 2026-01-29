using DocuEye.Linter.Model;
using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Elements;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DocuEye.Structurizr.Json.Model.Maps
{
    public static class StructurizrJsonSoftwareSystemMap
    {
        public static ElementToImport ConvertToApModel(this StructurizrJsonSoftwareSystem source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new ElementToImport
            {
                StructurizrId = source.Id ?? string.Empty,
                Type = ElementType.SoftwareSystem,
                Tags = string.IsNullOrWhiteSpace(source.Tags) ? null : source.Tags.Split(",").ToArray(),
                Name = source.Name ?? string.Empty,
                Description = source.Description,
                Location = source.Location,
                Url = source.Url,
                Group = source.Group,
                Properties = source.Properties ?? new Dictionary<string, string>(),
                DslId = source.DslId,
                OwnerTeam = source.OwnerTeam,
                SourceCodeUrl = source.SourceCodeUrl
            };
        }

        public static LinterModelElement ToLinterModelElement(this StructurizrJsonSoftwareSystem source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new LinterModelElement()
            {
                Identifier = source.DslId,
                Name = source.Name ?? string.Empty,
                Tags = string.IsNullOrWhiteSpace(source.Tags) ? new List<string>() : source.Tags.Split(",").ToList(),
                Description = source.Description,
                Properties = source.Properties != null ? new Dictionary<string, string>(source.Properties) : new Dictionary<string, string>(),
                JsonModelId = source.Id
            };
        }
    }
}
