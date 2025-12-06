using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Views;
using System;

namespace DocuEye.Structurizr.Json.Model.Maps
{
    public static class StructurizrJsonSystemLandscapeViewMap
    {
        public static ViewToImport ToViewToImport(this StructurizrJsonSystemLandscapeView source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new ViewToImport
            {
                ViewType = ViewType.SystemLandscapeView,
                Title = source.Title,
                Description = source.Description,
                Key = source.Key ?? string.Empty,
                PaperSize = source.PaperSize ?? string.Empty,
                AutomaticLayout = source.AutomaticLayout?.ToViewAutomaticLayoutToImport(),
                ExternalBoundariesVisible = source.EnterpriseBoundaryVisible,
                Elements = source.Elements?.ToElementInViewToImport() ?? Array.Empty<ElementInViewToImport>(),
                Relationships = source.Relationships?.ToRelationshipInViewToImport() ?? Array.Empty<RelationshipInViewToImport>()
            };
        }
    }
}
