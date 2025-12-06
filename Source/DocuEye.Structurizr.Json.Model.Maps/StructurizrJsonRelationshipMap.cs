using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Elements;
using DocuEye.WorkspaceImporter.Api.Model.Relationships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocuEye.Structurizr.Json.Model.Maps
{
    public static class StructurizrJsonRelationshipMap
    {
        public static RelationshipToImport ConvertToApiModel(this StructurizrJsonRelationship source)
        {

            if (source == null) throw new ArgumentNullException(nameof(source));
            return new RelationshipToImport
            {
                StructurizrId = source.Id ?? string.Empty,
                Tags = string.IsNullOrWhiteSpace(source.Tags) ? null : source.Tags.Split(",").ToArray(),
                Description = source.Description,
                Url = source.Url,
                Properties = source.Properties ?? new Dictionary<string, string>(),
                DslId = source.DslId,
                InteractionStyle = source.InteractionStyle,
                Technology = source.Technology,
                StructurizrSourceId = source.SourceId,
                StructurizrDestinationId = source.DestinationId,
                StructurizrLinkedRelationshipId = source.LinkedRelationshipId
            };
            
        }
    }
}
