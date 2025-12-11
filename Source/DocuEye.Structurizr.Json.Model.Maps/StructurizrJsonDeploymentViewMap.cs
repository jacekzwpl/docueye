using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocuEye.Structurizr.Json.Model.Maps
{
    public static class StructurizrJsonDeploymentViewMap
    {
        public static ViewToImport ToViewToImport(this StructurizrJsonDeploymentView source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var view = new ViewToImport
            {
                ViewType = ViewType.DeploymentView,
                Key = source.Key ?? string.Empty,
                Title = source.Title,
                Description = source.Description,
                StructurizrElementId = source.SoftwareSystemId,
                PaperSize = source.PaperSize,
                AutomaticLayout = source.AutomaticLayout?.ToViewAutomaticLayoutToImport(),
                Elements = source.Elements?.ToElementInViewToImport() ?? Array.Empty<ElementInViewToImport>(),
                Relationships = source.Relationships?.ToRelationshipInViewToImport() ?? Array.Empty<RelationshipInViewToImport>()
            };
            return view;
        }

        public static IEnumerable<ViewToImport> ToViewToImport(this IEnumerable<StructurizrJsonDeploymentView> source)
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
