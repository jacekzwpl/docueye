using DocuEye.WorkspaceImporter.Api.Model.Elements;
using DocuEye.ModelKeeper.Model;
using System;
using System.Collections.Generic;

namespace DocuEye.WorkspaceImporter.Api.Model.Maps
{
    public static class ElementToImportMap
    {
        public static Element ToElement(this ElementToImport source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new Element
            {
                Name = source.Name,
                Description = source.Description,
                Technology = source.Technology,
                Tags = source.Tags ?? Array.Empty<string>(),
                Type = source.Type,
                Url = source.Url,
                Group = source.Group,
                DslId = source.DslId,
                Properties = source.Properties ?? new Dictionary<string, string>(),
                SourceCodeUrl = source.SourceCodeUrl,
                OwnerTeam = source.OwnerTeam,
                StructurizrId = source.StructurizrId,
                Location = source.Location
            };
        }

        public static IEnumerable<Element> ToElements(this IEnumerable<ElementToImport> sources)
        {
            if (sources == null) throw new ArgumentNullException(nameof(sources));
            foreach (var s in sources) yield return s.ToElement();
        }
    }
}
