using DocuEye.Linter.Model;
using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DocuEye.Structurizr.Json.Model.Maps
{
    public static class StructurizrJsonDeploymentNodeMap
    {
        public static ElementToImport ConvertToApModel(this StructurizrJsonDeploymentNode source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new ElementToImport
            {
                StructurizrId = source.Id ?? string.Empty,
                Type = ElementType.DeploymentNode,
                Tags = string.IsNullOrWhiteSpace(source.Tags) ? null : source.Tags.Split(",").ToArray(),
                Name = source.Name ?? string.Empty,
                Description = source.Description,
                Url = source.Url,
                Group = source.Group,
                Properties = source.Properties ?? new Dictionary<string, string>(),
                DslId = source.DslId,
                OwnerTeam = source.OwnerTeam,
                SourceCodeUrl = source.SourceCodeUrl,
                Technology = source.Technology
            };
        }

        public static LinterModelElement ToLinterModelElement(this StructurizrJsonDeploymentNode source, string? parentIdentifier)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new LinterModelElement()
            {
                Identifier = source.DslId,
                Name = source.Name ?? string.Empty,
                Tags = string.IsNullOrWhiteSpace(source.Tags) ? new List<string>() : source.Tags.Split(",").ToList(),
                Description = source.Description,
                Properties = source.Properties != null ? new Dictionary<string, string>(source.Properties) : new Dictionary<string, string>(),
                Technology = source.Technology,
                ParentIdentifier = parentIdentifier,
                JsonModelId = source.Id
            };
        }

        public static IEnumerable<LinterModelElement> ToLinterModelElements(this StructurizrJsonDeploymentNode source, string? parentIdentifier, IEnumerable<LinterModelElement> currentElements)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var elements = new List<LinterModelElement>
            {
                source.ToLinterModelElement(parentIdentifier)
            };
            if (source.Children != null)
            {
                foreach (var childNode in source.Children)
                {
                    elements.AddRange(childNode.ToLinterModelElements(source.DslId, currentElements));
                }
            }
            source.ContainerInstances?.ToList().ForEach(ci =>
            {
                var existingElement = currentElements.FirstOrDefault(e => e.JsonModelId == ci.ContainerId);
                if (existingElement != null)
                {
                    elements.Add(ci.ToLinterModelElement(source.DslId, existingElement));
                }
            });

            source.SoftwareSystemInstances?.ToList().ForEach(ssi =>
            {
                var existingElement = currentElements.FirstOrDefault(e => e.JsonModelId == ssi.SoftwareSystemId);
                if (existingElement != null)
                {
                    elements.Add(ssi.ToLinterModelElement(source.DslId, existingElement));
                }
            });
            source.InfrastructureNodes?.ToList().ForEach(inode =>
            {
                elements.Add(inode.ToLinterModelElement(source.DslId));
            });

            
            return elements;
        }

        public static IEnumerable<StructurizrJsonRelationship> GetAllRelationships(this StructurizrJsonDeploymentNode source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var relationships = new List<StructurizrJsonRelationship>();
            if (source.Relationships != null)
            {
                relationships.AddRange(source.Relationships);
            }
            if (source.Children != null)
            {
                foreach (var childNode in source.Children)
                {
                    relationships.AddRange(childNode.GetAllRelationships());
                }
            }

            source.ContainerInstances?.ToList().ForEach(ci =>
            {
                relationships.AddRange(ci.Relationships ?? Array.Empty<StructurizrJsonRelationship>());
            });

            source.SoftwareSystemInstances?.ToList().ForEach(ssi =>
            {
                relationships.AddRange(ssi.Relationships ?? Array.Empty<StructurizrJsonRelationship>());
            });
            source.InfrastructureNodes?.ToList().ForEach(inode =>
            {
                relationships.AddRange(inode.Relationships ?? Array.Empty<StructurizrJsonRelationship>());
            });

            return relationships;
        }
    }
}
