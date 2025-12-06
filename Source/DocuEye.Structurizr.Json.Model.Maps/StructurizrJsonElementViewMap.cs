using DocuEye.WorkspaceImporter.Api.Model.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocuEye.Structurizr.Json.Model.Maps
{
    public static class StructurizrJsonElementViewMap
    {
        public static ElementInViewToImport ToElementInViewToImport(this StructurizrJsonElementView source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new ElementInViewToImport
            {
                StructurizrId = source.Id ?? string.Empty,
                X = source.X,
                Y = source.Y
            };
        }

        public static IEnumerable<ElementInViewToImport> ToElementInViewToImport(this IEnumerable<StructurizrJsonElementView> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var result = new List<ElementInViewToImport>();
            foreach (var item in source)
            {
                result.Add(item.ToElementInViewToImport());
            }
            return result.AsEnumerable();
        }
    }
}
