using DocuEye.ViewsKeeper.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.ModelKeeper.Model.Maps
{
    public static class RelationshipMap
    {
        public static RelationshipView MapToRelationshipView(this Relationship source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new RelationshipView
            {
                WorkspaceId = source.WorkspaceId,
                Url = source.Url,
                Description = source.Description,
                DestinationId = source.DestinationId,
                DslId = source.DslId,
                Id = source.Id,
                InteractionStyle = source.InteractionStyle,
                LinkedRelationshipId = source.LinkedRelationshipId,
                Properties = source.Properties,
                SourceId = source.SourceId,
                Tags = source.Tags,
                Technology = source.Technology
            };
        }
    }
}
