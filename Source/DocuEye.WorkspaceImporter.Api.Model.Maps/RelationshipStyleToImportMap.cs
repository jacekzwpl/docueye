using DocuEye.WorkspaceImporter.Api.Model.ViewConfiguration;
using DocuEye.WorkspacesKeeper.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Api.Model.Maps
{
    public static class RelationshipStyleToImportMap
    {
        public static RelationshipStyle ToRelationshipStyle(this RelationshipStyleToImport source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new RelationshipStyle
            {
                FontSize = source.FontSize,
                Color = source.Color,
                Dashed = source.Dashed,
                Opacity = source.Opacity,
                Position = source.Position,
                Routing = source.Routing,
                Tag = source.Tag,
                Thickness = source.Thickness,
                Width = source.Width
            };
        }

        public static IEnumerable<RelationshipStyle> ToRelationshipStyles(this IEnumerable<RelationshipStyleToImport> sources)
        {
            if (sources == null) throw new ArgumentNullException(nameof(sources));
            var result = new List<RelationshipStyle>();
            foreach (var s in sources) result.Add(s.ToRelationshipStyle());
            return result.ToArray();
        }
    }
}
