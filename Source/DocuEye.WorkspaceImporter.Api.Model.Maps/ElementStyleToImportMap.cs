using DocuEye.WorkspaceImporter.Api.Model.ViewConfiguration;
using DocuEye.WorkspacesKeeper.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Api.Model.Maps
{
    public static class ElementStyleToImportMap
    {
        public static ElementStyle ToElementStyle(this ElementStyleToImport source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new ElementStyle
            {
                Tag = source.Tag,
                Width = source.Width,
                Height = source.Height,
                Background = source.Background,
                Stroke = source.Stroke,
                StrokeWidth = source.StrokeWidth,
                Color = source.Color,
                FontSize = source.FontSize,
                Shape = source.Shape,
                Icon = source.Icon,
                Border = source.Border,
                Opacity = source.Opacity,
                Metadata = source.Metadata,
                Description = source.Description
            };
        }

        public static IEnumerable<ElementStyle> ToElementStyles(this IEnumerable<ElementStyleToImport> sources)
        {
            if (sources == null) throw new ArgumentNullException(nameof(sources));
            foreach (var s in sources) yield return s.ToElementStyle();
        }
    }
}
