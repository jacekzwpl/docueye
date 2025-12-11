using DocuEye.WorkspaceImporter.Api.Model.Docs;
using DocuEye.DocsKeeper.Model;
using System;
using System.Collections.Generic;

namespace DocuEye.WorkspaceImporter.Api.Model.Maps
{
    public static class DocumentationSectionToImportMap
    {
        public static DocumentationSection MapToDocumentationSection(this DocumentationSectionToImport source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new DocumentationSection
            {
                Format = source.Format,
                Content = source.Content,
                Order = source.Order
            };
        }


        public static IEnumerable<DocumentationSection> MapToDocumentationSections(this IEnumerable<DocumentationSectionToImport> sources)
        {
            if (sources == null) throw new ArgumentNullException(nameof(sources));
            foreach (var s in sources) yield return s.MapToDocumentationSection();
        }




        

    
    }
}
