using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Views;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public static IEnumerable<ViewToImport> ToViewToImport(this IEnumerable<StructurizrJsonSystemLandscapeView> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var result = new List<ViewToImport>();
            foreach (var item in source)
            {
                result.Add(item.ToViewToImport());
            }
            return result.AsEnumerable();
        }
    }
}
