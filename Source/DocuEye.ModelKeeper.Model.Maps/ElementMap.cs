using DocuEye.ModelKeeper.Application.Model;
using DocuEye.ViewsKeeper.Model;
using System;

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
    }
}
