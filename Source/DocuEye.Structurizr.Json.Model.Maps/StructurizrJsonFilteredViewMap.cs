using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocuEye.Structurizr.Json.Model.Maps
{
    public static class StructurizrJsonFilteredViewMap
    {
        public static ViewToImport ToViewToImport(this StructurizrJsonFilteredView source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var view = new ViewToImport
            {
                ViewType = ViewType.FilteredView,
                Key = source.Key ?? string.Empty,
                Title = source.Title,
                Description = source.Description,
                BaseViewKey = source.BaseViewKey,
                Tags = source.Tags,
                Mode = source.Mode
            };
            return view;
        }

        public static IEnumerable<ViewToImport> ToViewToImport(this IEnumerable<StructurizrJsonFilteredView> source)
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
