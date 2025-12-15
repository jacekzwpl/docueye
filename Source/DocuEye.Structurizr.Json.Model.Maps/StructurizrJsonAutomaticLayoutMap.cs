using DocuEye.WorkspaceImporter.Api.Model.Views;
using System;

namespace DocuEye.Structurizr.Json.Model.Maps
{
    public static class StructurizrJsonAutomaticLayoutMap
    {
        public static ViewAutomaticLayout ToViewAutomaticLayoutToImport(this StructurizrJsonAutomaticLayout source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new ViewAutomaticLayout
            {
                Implementation = source.Implementation,
                RankDirection = source.RankDirection,
                RankSeparation = source.RankSeparation,
                NodeSeparation = source.NodeSeparation,
                EdgeSeparation = source.EdgeSeparation,
                Vertices = source.Vertices
            };
        }
    }
}
