using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Elements;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DocuEye.Structurizr.Json.Model.Maps
{
    public static class StructurizrJsonDeploymentNodeMap
    {
        public static ElementToImport ConvertToApModel(this StructurizrJsonDeploymentNode source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new ElementToImport
            {
                StructurizrId = source.Id ?? string.Empty,
                Type = ElementType.DeploymentNode,
                Tags = string.IsNullOrWhiteSpace(source.Tags) ? null : source.Tags.Split(",").ToArray(),
                Name = source.Name ?? string.Empty,
                Description = source.Description,
                Url = source.Url,
                Group = source.Group,
                Properties = source.Properties ?? new Dictionary<string, string>(),
                DslId = source.DslId,
                OwnerTeam = source.OwnerTeam,
                SourceCodeUrl = source.SourceCodeUrl,
                Technology = source.Technology
            };
        }
    }
}
