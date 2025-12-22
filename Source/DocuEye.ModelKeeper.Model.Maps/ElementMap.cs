using DocuEye.ModelKeeper.Application.Model;
using DocuEye.ViewsKeeper.Model;
using System;
using System.Collections.Generic;

namespace DocuEye.ModelKeeper.Model.Maps
{
    public static class ElementMap
    {

        public static ElementView MapToElementView(this Element source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new ElementView
            {
                Name = source.Name,
                Id = source.Id,
                Description = source.Description,
                DslId = source.DslId,
                Group = source.Group,
                InstanceOfId = source.InstanceOfId,
                Location = source.Location,
                OwnerTeam = source.OwnerTeam,
                ParentId = source.ParentId,
                Properties = source.Properties,
                SourceCodeUrl = source.SourceCodeUrl,
                Tags = source.Tags,
                Technology = source.Technology,
                Type = source.Type,
                Url = source.Url,
                WorkspaceId = source.WorkspaceId,
            };
        }

        public static WorkspaceCatalogElement MapToWorkspaceCatalogElement(this Element source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new WorkspaceCatalogElement
            {
                Id = source.Id,
                Name = source.Name,
                Type = source.Type,
                Description = source.Description
            };
        }

        public static IEnumerable<WorkspaceCatalogElement> MapToWorkspaceCatalogElements(this IEnumerable<Element> sources)
        {
            if (sources == null) throw new ArgumentNullException(nameof(sources));
            var result = new List<WorkspaceCatalogElement>();
            foreach (var s in sources) result.Add(s.MapToWorkspaceCatalogElement());
            return result.ToArray();
        }

        public static ChildElement MapToChildElement(this Element source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new ChildElement
            {
                Id = source.Id,
                Name = source.Name,
                Type = source.Type,
                Description = source.Description,
                InstanceOfId = source.InstanceOfId
            };
        }

        public static IEnumerable<ChildElement> MapToChildElements(this IEnumerable<Element> sources)
        {
            if (sources == null) throw new ArgumentNullException(nameof(sources));
            var result = new List<ChildElement>();
            foreach (var s in sources) result.Add(s.MapToChildElement());
            return result.ToArray();
        }

        public static ElementDependence MapToElementDependence(this Element source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new ElementDependence
            {
                Id = source.Id,
                Name = source.Name,
                Type = source.Type,
                Description = source.Description
            };
        }

        public static IEnumerable<ElementDependence> MapToElementDependences(this IEnumerable<Element> sources)
        {
            if (sources == null) throw new ArgumentNullException(nameof(sources));
            var result = new List<ElementDependence>();
            foreach (var s in sources) result.Add(s.MapToElementDependence());
            return result.ToArray();
        }

        public static ElementConsumer MapToElementConsumer(this Element source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new ElementConsumer
            {
                Id = source.Id,
                Name = source.Name,
                Type = source.Type,
                Description = source.Description
            };
        }

        public static IEnumerable<ElementConsumer> MapToElementConsumers(this IEnumerable<Element> sources)
        {
            if (sources == null) throw new ArgumentNullException(nameof(sources));
            var result = new List<ElementConsumer>();
            foreach (var s in sources) result.Add(s.MapToElementConsumer());
            return result.ToArray();
        }
    }
}
