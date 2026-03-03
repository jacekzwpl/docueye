using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Views;
using System;
using System.Collections.Generic;
using System.Linq;
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
                Properties = source.Properties != null ? new Dictionary<string, string>(source.Properties) : new Dictionary<string, string>(),
                Description = source.Description,
                StructurizrElementId = source.ElementId,
                DiagramEngine = source.DiagramEngine,
                ExternalBoundariesVisible = source.ExternalBoundariesVisible,
                PaperSize = source.PaperSize,
                AutomaticLayout = source.AutomaticLayout?.ToViewAutomaticLayoutToImport(),
                Elements = source.Elements?.ToElementInViewToImport() ?? Array.Empty<ElementInViewToImport>(),
                Relationships = source.Relationships?.ToRelationshipInViewToImport() ?? Array.Empty<RelationshipInViewToImport>()
            };
            return view;
        }

        public static IEnumerable<ViewToImport> ToViewToImport(this IEnumerable<StructurizrJsonDynamicView> source)
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
