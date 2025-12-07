using DocuEye.DocsKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Docs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Api.Model.Maps
{
    public static class DocumentationToImportMap
    {
        public static Documentation ToDocumentation(this DocumentationToImport source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new Documentation
            {
                Id = source.Id,
                Sections = source.Sections?.ToDocumentationSections() ?? Array.Empty<DocumentationSection>()
            };
        }
    }
}
