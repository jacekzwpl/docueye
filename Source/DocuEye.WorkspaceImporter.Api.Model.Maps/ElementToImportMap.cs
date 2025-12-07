using DocuEye.WorkspaceImporter.Api.Model.Elements;
using DocuEye.ModelKeeper.Model;
using System;
using System.Collections.Generic;

namespace DocuEye.WorkspaceImporter.Api.Model.Maps
{
    public static class ElementToImportMap
    {
        public static Element MapToElement(this ElementToImport source, Element? existing = null)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            existing ??= new Element();

            existing.Name = source.Name;
            existing.Description = source.Description;
            existing.Technology = source.Technology;
            existing.Tags = source.Tags ?? Array.Empty<string>();
            existing.Type = source.Type;
            existing.Url = source.Url;
            existing.Group = source.Group;
            existing.DslId = source.DslId;
            existing.Properties = source.Properties ?? new Dictionary<string, string>();
            existing.SourceCodeUrl = source.SourceCodeUrl;
            existing.OwnerTeam = source.OwnerTeam;
            existing.StructurizrId = source.StructurizrId;
            existing.Location = source.Location;
            return existing;
        }

        public static IEnumerable<Element> MapToElements(this IEnumerable<ElementToImport> sources)
        {
            if (sources == null) throw new ArgumentNullException(nameof(sources));
            foreach (var s in sources) yield return s.MapToElement();
        }
    }
}
