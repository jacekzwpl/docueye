using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Json.Model.Maps
{
    public static class StructurizrJsonDynamicViewMap
    {
        public static ViewToImport ToViewToImport(this StructurizrJsonDynamicView source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var view = new ViewToImport
            {
                ViewType = ViewType.DynamicView,
                Key = source.Key ?? string.Empty,
                Title = source.Title,
                Description = source.Description,
                StructurizrElementId = source.ElementId,
                ExternalBoundariesVisible = source.ExternalBoundariesVisible,
                PaperSize = source.PaperSize,
                Elements = source.Elements?.ToElementInViewToImport() ?? Array.Empty<ElementInViewToImport>(),
                Relationships = source.Relationships?.ToRelationshipInViewToImport() ?? Array.Empty<RelationshipInViewToImport>()
            };
            return view;
        }
    }
}
