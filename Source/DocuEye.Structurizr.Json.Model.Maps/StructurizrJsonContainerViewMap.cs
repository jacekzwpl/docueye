using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocuEye.Structurizr.Json.Model.Maps
{
    public static class StructurizrJsonContainerViewMap
    {
        public static ViewToImport ToViewToImport(this StructurizrJsonContainerView source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var view = new ViewToImport
            {
                ViewType = ViewType.ContainerView,
                Key = source.Key ?? string.Empty,
                Title = source.Title,
                Description = source.Description,
                StructurizrElementId = source.SoftwareSystemId,
                PaperSize = source.PaperSize,
                AutomaticLayout = source.AutomaticLayout?.ToViewAutomaticLayoutToImport(),
                Elements = source.Elements?.ToElementInViewToImport() ?? Array.Empty<ElementInViewToImport>(),
                Relationships = source.Relationships?.ToRelationshipInViewToImport() ?? Array.Empty<RelationshipInViewToImport>(),
                ExternalBoundariesVisible = source.ExternalSoftwareSystemBoundariesVisible
            };
            return view;
        }

        public static IEnumerable<ViewToImport> ToViewToImport(this IEnumerable<StructurizrJsonContainerView> source)
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
