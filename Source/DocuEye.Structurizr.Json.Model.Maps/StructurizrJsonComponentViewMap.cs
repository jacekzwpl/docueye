using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Json.Model.Maps
{
    public static class StructurizrJsonComponentViewMap
    {
        public static ViewToImport ToViewToImport(this StructurizrJsonComponentView source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var view = new ViewToImport
            {
                ViewType = ViewType.ComponentView,
                Key = source.Key ?? string.Empty,
                Title = source.Title,
                Description = source.Description,
                StructurizrElementId = source.ContainerId,
                PaperSize = source.PaperSize,
                AutomaticLayout = source.AutomaticLayout?.ToViewAutomaticLayoutToImport(),
                Elements = source.Elements?.ToElementInViewToImport() ?? Array.Empty<ElementInViewToImport>(),
                Relationships = source.Relationships?.ToRelationshipInViewToImport() ?? Array.Empty<RelationshipInViewToImport>(),
                ExternalBoundariesVisible = source.ExternalContainerBoundariesVisible
            };
            return view;
        }
    }
}
