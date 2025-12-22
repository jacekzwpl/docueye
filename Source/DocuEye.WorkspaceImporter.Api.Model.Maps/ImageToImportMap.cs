using DocuEye.DocsKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Docs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Api.Model.Maps
{
    public static class ImageToImportMap
    {
        public static Image MapToImage(this ImageToImport source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new Image
            {
                Name = source.Name,
                Content = source.Content,
                Type = source.Type,
                DocumentationId = source.DocumentationId
            };
        }

        public static IEnumerable<Image> MapToImages(this IEnumerable<ImageToImport> sources)
        {
            if (sources == null) throw new ArgumentNullException(nameof(sources));
            var result = new List<Image>();
            foreach (var s in sources) result.Add(s.MapToImage());
            return result.ToArray();
        }
    }
}
