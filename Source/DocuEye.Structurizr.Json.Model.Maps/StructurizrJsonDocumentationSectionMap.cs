using DocuEye.WorkspaceImporter.Api.Model.Docs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DocuEye.Structurizr.Json.Model.Maps
{
    public static class StructurizrJsonDocumentationSectionMap
    {
        public static DocumentationSectionToImport ConvertToApiModel(this StructurizrJsonDocumentationSection source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new DocumentationSectionToImport
            {
                Format = source.Format,
                Content = source.Content,
                Order = source.Order
            };
        }

        public static IEnumerable<DocumentationSectionToImport> ConvertToApiModel(this IEnumerable<StructurizrJsonDocumentationSection> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var result = new List<DocumentationSectionToImport>();
            foreach (var item in source)
            {
                result.Add(item.ConvertToApiModel());
            }
            return result.AsEnumerable();
           
        }
    }
}
