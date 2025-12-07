using DocuEye.WorkspaceImporter.Api.Model.Relationships;
using DocuEye.ModelKeeper.Model;
using System;
using System.Collections.Generic;

namespace DocuEye.WorkspaceImporter.Api.Model.Maps
{
    public static class RelationshipToImportMap
    {
        public static Relationship ToRelationship(this RelationshipToImport source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new Relationship
            {

                Description = source.Description,
                Technology = source.Technology,
                Tags = source.Tags ?? Array.Empty<string>(),
                Url = source.Url,
                Properties = source.Properties ?? new Dictionary<string, string>(),
                DslId = source.DslId,
                StructurizrId = source.StructurizrId,
                InteractionStyle = source.InteractionStyle,

            };
        }

        public static IEnumerable<Relationship> ToRelationships(this IEnumerable<RelationshipToImport> sources)
        {
            if (sources == null) throw new ArgumentNullException(nameof(sources));
            foreach (var s in sources) yield return s.ToRelationship();
        }
    }
}
