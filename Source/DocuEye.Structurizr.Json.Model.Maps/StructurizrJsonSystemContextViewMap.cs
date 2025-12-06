using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Views;
using System;

namespace DocuEye.Structurizr.Json.Model.Maps
{
    public static class StructurizrJsonSystemContextViewMap
    {
        public static ViewToImport ToViewToImport(this StructurizrJsonSystemContextView source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var view = new ViewToImport
            {
                ViewType = ViewType.SystemContextView,
                Key = source.Key ?? string.Empty,
                Title = source.Title,
                Description = source.Description,
                StructurizrElementId = source.SoftwareSystemId,
                PaperSize = source.PaperSize,
                AutomaticLayout = source.AutomaticLayout?.ToViewAutomaticLayoutToImport(),
                ExternalBoundariesVisible = source.EnterpriseBoundaryVisible,
                Elements = source.Elements?.ToElementInViewToImport() ?? Array.Empty<ElementInViewToImport>(),
                Relationships = source.Relationships?.ToRelationshipInViewToImport() ?? Array.Empty<RelationshipInViewToImport>()
            };
            return view;
        }
    }
}
