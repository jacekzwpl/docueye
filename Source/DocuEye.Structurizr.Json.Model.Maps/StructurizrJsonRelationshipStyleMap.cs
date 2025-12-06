using DocuEye.WorkspaceImporter.Api.Model.ViewConfiguration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Json.Model.Maps
{
    public static class StructurizrJsonRelationshipStyleMap
    {
        public static RelationshipStyleToImport ToRelationshipStyleToImport(this StructurizrJsonRelationshipStyle source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new RelationshipStyleToImport
            {
                FontSize = source.FontSize,
                Opacity = source.Opacity,
                Routing = source.Routing,
                Tag = source.Tag ?? string.Empty,
                Thickness = source.Thickness,
                Width = source.Width,
                Color = source.Color,
                Dashed = source.Dashed,
                Position = source.Position
            };
        }
    }
}
