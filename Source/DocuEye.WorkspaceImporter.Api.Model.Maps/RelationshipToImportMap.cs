using DocuEye.WorkspaceImporter.Api.Model.Relationships;
using DocuEye.ModelKeeper.Model;
using System;
using System.Collections.Generic;

namespace DocuEye.WorkspaceImporter.Api.Model.Maps
{
    public static class RelationshipToImportMap
    {
        public static Relationship MapToRelationship(this RelationshipToImport source, Relationship? existing = null)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            existing ??= new Relationship();
            existing.Description = source.Description;
            existing.Technology = source.Technology;
            existing.Tags = source.Tags ?? Array.Empty<string>();
            existing.Url = source.Url;
            existing.Properties = source.Properties ?? new Dictionary<string, string>();
            existing.DslId = source.DslId;
            existing.StructurizrId = source.StructurizrId;
            existing.InteractionStyle = source.InteractionStyle;
            return existing;
        }

        public static IEnumerable<Relationship> MapToRelationships(this IEnumerable<RelationshipToImport> sources)
        {
            if (sources == null) throw new ArgumentNullException(nameof(sources));
            foreach (var s in sources) yield return s.MapToRelationship();
        }
    }
}
