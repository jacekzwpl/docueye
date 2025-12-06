using DocuEye.WorkspaceImporter.Api.Model.Docs;
using System;

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
    }
}
