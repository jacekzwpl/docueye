using DocuEye.Linter.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DocuEye.Structurizr.Json.Model.Maps
{
    public static class StructurizrJsonModelMap
    {
        public static IEnumerable<LinterModelElement> ToLinterModelElements(this StructurizrJsonModel source)
        {
            var elements = new List<LinterModelElement>();
            if (source.SoftwareSystems != null)
            {
                foreach (var softwareSystem in source.SoftwareSystems)
                {
                    elements.Add(softwareSystem.ToLinterModelElement());
                    foreach (var container in softwareSystem.Containers ?? Array.Empty<StructurizrJsonContainer>())
                    {
                        elements.Add(container.ToLinterModelElement(softwareSystem.DslId));
                        foreach (var component in container.Components ?? Array.Empty<StructurizrJsonComponent>())
                        {
                            elements.Add(component.ToLinterModelElement(container.DslId));
                        }
                    }
                }
            }
            if (source.People != null)
            {
                foreach (var person in source.People)
                {
                    elements.Add(person.ToLinterModelElement());
                }
            }
            if (source.DeploymentNodes != null)
            {
                foreach (var deploymentNode in source.DeploymentNodes)
                {
                    elements.AddRange(deploymentNode.ToLinterModelElements(null, elements));
                }
            }
            
            if (source.CustomElements != null)
            {
                foreach (var customElement in source.CustomElements)
                {
                    elements.Add(customElement.ToLinterModelElement());
                }
            }
            return elements;
        }

        public static IEnumerable<LinterModelRelationship> ToLinterModelRelationships(this StructurizrJsonModel source, IEnumerable<LinterModelElement> elements)
        {
            var jsonRelationships = new List<StructurizrJsonRelationship>();
            var relationships = new List<LinterModelRelationship>();


            if (source.SoftwareSystems != null)
            {
                foreach (var softwareSystem in source.SoftwareSystems)
                {
                    jsonRelationships.AddRange(softwareSystem.Relationships ?? Array.Empty<StructurizrJsonRelationship>());
                    
                    foreach (var container in softwareSystem.Containers ?? Array.Empty<StructurizrJsonContainer>())
                    {
                        jsonRelationships.AddRange(container.Relationships ?? Array.Empty<StructurizrJsonRelationship>());
                        foreach (var component in container.Components ?? Array.Empty<StructurizrJsonComponent>())
                        {
                            jsonRelationships.AddRange(component.Relationships ?? Array.Empty<StructurizrJsonRelationship>());
                        }
                    }
                }
            }

            if (source.People != null)
            {
                foreach (var person in source.People)
                {
                    jsonRelationships.AddRange(person.Relationships ?? Array.Empty<StructurizrJsonRelationship>());
                }
            }
            if (source.DeploymentNodes != null)
            {
                foreach (var deploymentNode in source.DeploymentNodes)
                {
                    jsonRelationships.AddRange(deploymentNode.GetAllRelationships());
                }
            }

            if (source.CustomElements != null)
            {
                foreach (var customElement in source.CustomElements)
                {
                    jsonRelationships.AddRange(customElement.Relationships ?? Array.Empty<StructurizrJsonRelationship>());
                }
            }

            var definedRelationships = jsonRelationships.Where(o => string.IsNullOrEmpty(o.LinkedRelationshipId)).ToArray();
            var linkedRelationships = jsonRelationships.Where(o => !string.IsNullOrEmpty(o.LinkedRelationshipId)).ToArray();
            relationships.AddRange(definedRelationships.ToLinterModelRelationships(elements));
            relationships.AddRange(linkedRelationships.ToLinterModelRelationships(elements, relationships));


            return relationships;
        }


    }
}
