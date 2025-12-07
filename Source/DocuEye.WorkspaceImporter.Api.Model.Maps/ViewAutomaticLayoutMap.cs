using DocuEye.WorkspaceImporter.Api.Model.Views;
using DocuEye.ViewsKeeper.Model;
using System;

namespace DocuEye.WorkspaceImporter.Api.Model.Maps
{
    public static class ViewAutomaticLayoutMap
    {
        public static AutomaticLayout ToAutomaticLayout(this ViewAutomaticLayout source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new AutomaticLayout
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
