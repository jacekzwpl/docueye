using DocuEye.WorkspaceImporter.Api.Model.ViewConfiguration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Json.Model.Maps
{
    public static class StructurizrJsonElementStyleMap
    {
        public static ElementStyleToImport ToElementStyleToImport(this StructurizrJsonElementStyle source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new ElementStyleToImport
            {
                Background = source.Background,
                Border = source.Border,
                Color = source.Color,
                Description = source.Description,
                FontSize = source.FontSize,
                Height = source.Height,
                Icon = source.Icon,
                Metadata = source.Metadata,
                Opacity = source.Opacity,
                Shape = source.Shape,
                Stroke = source.Stroke,
                StrokeWidth = source.StrokeWidth,
                Tag = source.Tag,
                Width = source.Width
            };
        }
    }
}
