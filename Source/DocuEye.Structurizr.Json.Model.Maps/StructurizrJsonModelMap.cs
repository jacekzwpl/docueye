using DocuEye.Linter.Model;
using System;
using System.Collections.Generic;
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
                    elements.Add(deploymentNode.ToLinterModelElement(null));
                }
            }
            /*
            if (source.CustomElements != null)
            {
                foreach (var customElement in source.CustomElements)
                {
                    elements.Add(customElement.ToLinterModelElement());
                }
            }*/
            return elements;
        }
    }
}
