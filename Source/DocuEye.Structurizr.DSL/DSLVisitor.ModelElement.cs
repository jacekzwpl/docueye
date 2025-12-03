using Antlr4.Runtime.Misc;
using DocuEye.Structurizr.DSL.Model;
using static StructurizrDSLParser;

namespace DocuEye.Structurizr.DSL
{
    public partial class DSLVisitor
    {

        public void CheckModelElementBodyProperty(string property, StructurizrModelElementType elementType, int line, int column)
        {
            bool isValid = true;
            switch (property) {
                case "Tags":
                    if(!new StructurizrModelElementType[] { 
                        StructurizrModelElementType.Person,
                        StructurizrModelElementType.CustomElement,
                        StructurizrModelElementType.SoftwareSystem,
                        StructurizrModelElementType.Container,
                        StructurizrModelElementType.Component,
                        StructurizrModelElementType.DeploymentNode,
                        StructurizrModelElementType.InfrastructureNode,
                        StructurizrModelElementType.SoftwareSystemInstance,
                        StructurizrModelElementType.ContainerInstance,
                        StructurizrModelElementType.Relationship
                    }.Contains(elementType))
                    {
                        isValid = false;                        
                    }
                    break;
                case "Description":
                    if (!new StructurizrModelElementType[] {
                        StructurizrModelElementType.Person,
                        StructurizrModelElementType.CustomElement,
                        StructurizrModelElementType.SoftwareSystem,
                        StructurizrModelElementType.Container,
                        StructurizrModelElementType.Component,
                        StructurizrModelElementType.DeploymentNode,
                        StructurizrModelElementType.InfrastructureNode,
                        StructurizrModelElementType.SoftwareSystemInstance,
                        StructurizrModelElementType.ContainerInstance,
                        StructurizrModelElementType.Relationship
                    }.Contains(elementType))
                    {
                        isValid = false;
                    }
                    break;
                case "Properties":
                    if (!new StructurizrModelElementType[] {
                        StructurizrModelElementType.Person,
                        StructurizrModelElementType.CustomElement,
                        StructurizrModelElementType.SoftwareSystem,
                        StructurizrModelElementType.Container,
                        StructurizrModelElementType.Component,
                        StructurizrModelElementType.DeploymentNode,
                        StructurizrModelElementType.InfrastructureNode,
                        StructurizrModelElementType.SoftwareSystemInstance,
                        StructurizrModelElementType.ContainerInstance,
                        StructurizrModelElementType.Relationship
                    }.Contains(elementType))
                    {
                        isValid = false;
                    }
                    break;
                case "Url":
                    if (!new StructurizrModelElementType[] {
                        StructurizrModelElementType.Person,
                        StructurizrModelElementType.CustomElement,
                        StructurizrModelElementType.SoftwareSystem,
                        StructurizrModelElementType.Container,
                        StructurizrModelElementType.Component,
                        StructurizrModelElementType.DeploymentNode,
                        StructurizrModelElementType.InfrastructureNode,
                        StructurizrModelElementType.SoftwareSystemInstance,
                        StructurizrModelElementType.ContainerInstance,
                        StructurizrModelElementType.Relationship
                    }.Contains(elementType))
                    {
                        isValid = false;
                    }
                    break;
                case "Relationship":
                    if (!new StructurizrModelElementType[] {
                        StructurizrModelElementType.Person,
                        StructurizrModelElementType.CustomElement,
                        StructurizrModelElementType.SoftwareSystem,
                        StructurizrModelElementType.Container,
                        StructurizrModelElementType.Component,
                        StructurizrModelElementType.DeploymentNode,
                        StructurizrModelElementType.InfrastructureNode,
                        StructurizrModelElementType.SoftwareSystemInstance,
                        StructurizrModelElementType.ContainerInstance,
                        StructurizrModelElementType.DeploymentEnvironment
                    }.Contains(elementType))
                    {
                        isValid = false;
                    }
                    break;
                case "Perspectives":
                    if (!new StructurizrModelElementType[] {
                        StructurizrModelElementType.Person,
                        StructurizrModelElementType.CustomElement,
                        StructurizrModelElementType.SoftwareSystem,
                        StructurizrModelElementType.Container,
                        StructurizrModelElementType.Component,
                        StructurizrModelElementType.DeploymentNode,
                        StructurizrModelElementType.InfrastructureNode,
                        StructurizrModelElementType.SoftwareSystemInstance,
                        StructurizrModelElementType.ContainerInstance,
                        StructurizrModelElementType.Relationship
                    }.Contains(elementType))
                    {
                        isValid = false;
                    }
                    break;
                case "Docs":
                    if (!new StructurizrModelElementType[] {
                        StructurizrModelElementType.SoftwareSystem,
                        StructurizrModelElementType.Container,
                        StructurizrModelElementType.Component
                    }.Contains(elementType))
                    {
                        isValid = false;
                    }
                    break;
                case "Adrs":
                    if (!new StructurizrModelElementType[] {
                        StructurizrModelElementType.SoftwareSystem,
                        StructurizrModelElementType.Container,
                        StructurizrModelElementType.Component
                    }.Contains(elementType))
                    {
                        isValid = false;
                    }
                    break;
                case "Container":
                    if (!new StructurizrModelElementType[] {
                        StructurizrModelElementType.SoftwareSystem
                    }.Contains(elementType))
                    {
                        isValid = false;
                    }
                    break;
                case "Group":
                    if (!new StructurizrModelElementType[] {
                        StructurizrModelElementType.SoftwareSystem,
                        StructurizrModelElementType.DeploymentEnvironment,
                        StructurizrModelElementType.Container,
                        StructurizrModelElementType.DeploymentNode
                    }.Contains(elementType))
                    {
                        isValid = false;
                    }
                    break;
                case "Technology":
                    if (!new StructurizrModelElementType[] {
                        StructurizrModelElementType.Container,
                        StructurizrModelElementType.Component,
                        StructurizrModelElementType.DeploymentNode,
                        StructurizrModelElementType.InfrastructureNode,
                        StructurizrModelElementType.Relationship
                    }.Contains(elementType))
                    {
                        isValid = false;
                    }
                    break;
                case "Component":
                    if (!new StructurizrModelElementType[] {
                        StructurizrModelElementType.Container
                    }.Contains(elementType))
                    {
                        isValid = false;
                    }
                    break;
                case "DeploymentNode":
                    if (!new StructurizrModelElementType[] {
                        StructurizrModelElementType.DeploymentEnvironment,
                        StructurizrModelElementType.DeploymentNode
                    }.Contains(elementType))
                    {
                        isValid = false;
                    }
                    break;
                case "DeploymentGroup":
                    if (!new StructurizrModelElementType[] {
                        StructurizrModelElementType.DeploymentEnvironment
                    }.Contains(elementType))
                    {
                        isValid = false;
                    }
                    break;
                case "InfrastructureNode":
                    if (!new StructurizrModelElementType[] {
                        StructurizrModelElementType.DeploymentNode
                    }.Contains(elementType))
                    {
                        isValid = false;
                    }
                    break;
                case "SoftwareSystemInstance":
                    if (!new StructurizrModelElementType[] {
                        StructurizrModelElementType.DeploymentNode
                    }.Contains(elementType))
                    {
                        isValid = false;
                    }
                    break;
                case "ContainerInstance":
                    if (!new StructurizrModelElementType[] {
                        StructurizrModelElementType.DeploymentNode
                    }.Contains(elementType))
                    {
                        isValid = false;
                    }
                    break;
                case "Instances":
                    if (!new StructurizrModelElementType[] {
                        StructurizrModelElementType.DeploymentNode
                    }.Contains(elementType))
                    {
                        isValid = false;
                    }
                    break;
                case "HealthCheck":
                    if (!new StructurizrModelElementType[] {
                        StructurizrModelElementType.SoftwareSystemInstance,
                        StructurizrModelElementType.ContainerInstance
                    }.Contains(elementType))
                    {
                        isValid = false;
                    }
                    break;


            }

            if (!isValid) {
                throw new Exception(string.Format(
                            "The '{0}' property is not allowed for elements of type '{1}' at {2}:{3}.",
                            property,
                            elementType,
                            line,
                            column));
            }
        }

        public StructurizrModelElement VisitModelElementBody([NotNull] ModelElementBodyContext context, StructurizrModelElement element, string? groupId = null)
        {
            if (context.children == null)
            {
                return element;
            }

            foreach (var childContext in context.children)
            {
                if(childContext is TagsBlockContext tagsBlockContext)
                {
                    this.CheckModelElementBodyProperty("Tags", element.Type, tagsBlockContext.Start.Line, tagsBlockContext.Start.Column);
                    var tags = (List<string>)this.VisitTagsBlock(tagsBlockContext);
                    if (element.Tags == null)
                    {
                        element.Tags = new List<string>();
                    }
                    element.Tags.AddRange(tags);
                }

                if (childContext is DescriptionBlockContext descriptionBlockContext)
                {
                    this.CheckModelElementBodyProperty("Description", element.Type, descriptionBlockContext.Start.Line, descriptionBlockContext.Start.Column);
                    var description = (string?)this.VisitDescriptionBlocks(new DescriptionBlockContext[] { descriptionBlockContext });
                    element.Description = description;
                }

                if (childContext is PropertiesContext propertiesBlockContext)
                {
                    this.CheckModelElementBodyProperty("Properties", element.Type, propertiesBlockContext.Start.Line, propertiesBlockContext.Start.Column);
                    var properties = (Dictionary<string, string>)this.VisitProperties(propertiesBlockContext);
                    if (element.Properties == null)
                    {
                        element.Properties = new Dictionary<string, string>();
                    }
                    foreach (var kvp in properties)
                    {
                        element.Properties[kvp.Key] = kvp.Value;
                    }
                }

                if (childContext is UrlBlockContext urlBlockContext)
                {
                    this.CheckModelElementBodyProperty("Url", element.Type, urlBlockContext.Start.Line, urlBlockContext.Start.Column);
                    var url = (string)this.VisitUrlBlock(urlBlockContext);
                    element.Url = url;
                }

                if (childContext is RelationshipContext relationshipContext)
                {
                    this.CheckModelElementBodyProperty("Relationship", element.Type, relationshipContext.Start.Line, relationshipContext.Start.Column);
                    var relationship = (StructurizrRelationship)this.VisitRelationship(relationshipContext, element.Identifier);
                    this.workspace.Model.AddRelationship(relationship);
                }

                if (childContext is PerspectivesContext perspectivesContext)
                {
                    this.CheckModelElementBodyProperty("Perspectives", element.Type, perspectivesContext.Start.Line, perspectivesContext.Start.Column);
                    var perspectives = (List<StructurizrPerspective>)this.VisitPerspectives(perspectivesContext);
                    if (element.Perspectives == null)
                    {
                        element.Perspectives = new List<StructurizrPerspective>();
                    }
                    (element.Perspectives as List<StructurizrPerspective>)?.AddRange(perspectives);
                }

                if (childContext is DocsKeywordContext docsKeywordContext)
                {
                    this.CheckModelElementBodyProperty("Docs", element.Type, docsKeywordContext.Start.Line, docsKeywordContext.Start.Column);
                    element.Docs = this.VisitDocsKeyword(new DocsKeywordContext[] { docsKeywordContext });
                }

                if (childContext is AdrsKeywordContext adrKeywordContext)
                {
                    this.CheckModelElementBodyProperty("Adrs", element.Type, adrKeywordContext.Start.Line, adrKeywordContext.Start.Column);
                    element.Adrs = this.VisitAdrsKeyword(new AdrsKeywordContext[] { adrKeywordContext });
                }

                if (childContext is ContainerContext containerContext)
                {
                    this.CheckModelElementBodyProperty("Container", element.Type, containerContext.Start.Line, containerContext.Start.Column);
                    var container = this.VisitContainer(containerContext, element.Identifier);
                    if(groupId != null)
                    {
                        container.GroupId = groupId;
                    }

                    if (this.workspace.Model.ElementIdentifierExists(container.Identifier))
                    {
                        throw new Exception(
                            string.Format(
                                "Duplicate container identifier '{0}' at {1}:{2}",
                                container.Identifier,
                                containerContext.Start.Line,
                                containerContext.Start.Column));
                    }
                    this.workspace.Model.AddModelElement(container);
                }

                if (childContext is ModelElementGroupContext groupContext)
                {
                    this.CheckModelElementBodyProperty("Group", element.Type, groupContext.Start.Line, groupContext.Start.Column);
                    var group = this.VisitModelElementGroup(groupContext, element, groupId);
                    this.workspace.Model.Groups.Add(group);
                    //var group = this.VisitModelElementGroup(groupContext)
                    //var groupContext = (ModelElementGroupContext)childContext;
                    //var group = this.VisitModelElementGroup(groupContext, element.Identifier,element.Type, null);
                    //
                }

                if (childContext is IncludeFileContext includeFileContext)
                {
                    this.VisitIncludeFileModelElementBody(includeFileContext, element, groupId);
                }

                if (childContext is TechnologyBlockContext technologyBlockContext)
                {
                    this.CheckModelElementBodyProperty("Technology", element.Type, technologyBlockContext.Start.Line, technologyBlockContext.Start.Column);
                    var technology = (string?)this.VisitTechnologyBlocks(new TechnologyBlockContext[] { technologyBlockContext });
                    element.Technology = technology;
                }

                if (childContext is ComponentContext componentContext)
                {
                    this.CheckModelElementBodyProperty("Component", element.Type, componentContext.Start.Line, componentContext.Start.Column);
                    var component = this.VisitComponent(componentContext, element.Identifier);
                    if (groupId != null) { 
                        component.GroupId = groupId;
                    }
                    if (this.workspace.Model.ElementIdentifierExists(component.Identifier))
                    {
                        throw new Exception(
                            string.Format(
                                "Duplicate component identifier '{0}' at {1}:{2}",
                                component.Identifier,
                                componentContext.Start.Line,
                                componentContext.Start.Column));
                    }
                    this.workspace.Model.AddModelElement(component);
                }

                if (childContext is DeploymentGroupContext deploymentGroupContext)
                {
                    this.CheckModelElementBodyProperty("DeploymentGroup", element.Type, deploymentGroupContext.Start.Line, deploymentGroupContext.Start.Column);
                    var deploymentGroup = this.VisitDeploymentGroup(deploymentGroupContext, element.Identifier);
                    if (this.workspace.Model.ElementIdentifierExists(deploymentGroup.Identifier))
                    {
                        throw new Exception(
                            string.Format(
                                "Duplicate deploymentGroup identifier '{0}' at {1}:{2}",
                                deploymentGroup.Identifier,
                                deploymentGroupContext.Start.Line,
                                deploymentGroupContext.Start.Column));
                    }
                    this.workspace.Model.DeploymentGroups.Add(deploymentGroup);
                }

                if (childContext is DeploymentNodeContext deploymentNodeContext)
                {
                    this.CheckModelElementBodyProperty("DeploymentNode", element.Type, deploymentNodeContext.Start.Line, deploymentNodeContext.Start.Column);
                    
                    var deploymentNode = this.VisitDeploymentNode(
                            deploymentNodeContext, 
                            element.Type == StructurizrModelElementType.DeploymentEnvironment ? element.Identifier : element.DeploymentEnvironmentIdentifier,
                            element.Type == StructurizrModelElementType.DeploymentEnvironment ? null : element.Identifier);
                    if(groupId != null)
                    {
                        deploymentNode.GroupId = groupId;
                    }
                    
                    if (this.workspace.Model.ElementIdentifierExists(deploymentNode.Identifier))
                    {
                        throw new Exception(
                            string.Format(
                                "Duplicate deploymentNode identifier '{0}' at {1}:{2}",
                                deploymentNode.Identifier,
                                deploymentNodeContext.Start.Line,
                                deploymentNodeContext.Start.Column));
                    }
                    this.workspace.Model.AddModelElement(deploymentNode);
                }

                if (childContext is InfrastructureNodeContext infrastructureNodeContext)
                {
                    this.CheckModelElementBodyProperty("InfrastructureNode", element.Type, infrastructureNodeContext.Start.Line, infrastructureNodeContext.Start.Column);
                    var infrastructureNode = this.VisitInfrastructureNode(infrastructureNodeContext, element.Identifier);
                    if (groupId != null) { 
                        infrastructureNode.GroupId = groupId;
                    }
                    if (this.workspace.Model.ElementIdentifierExists(infrastructureNode.Identifier))
                    {
                        throw new Exception(
                            string.Format(
                                "Duplicate infrastructureNode identifier '{0}' at {1}:{2}",
                                infrastructureNode.Identifier,
                                infrastructureNodeContext.Start.Line,
                                infrastructureNodeContext.Start.Column));
                    }

                    this.workspace.Model.AddModelElement(infrastructureNode);
                }

                if (childContext is SoftwareSystemInstanceContext softwareSystemInstanceContext)
                {
                    this.CheckModelElementBodyProperty("SoftwareSystemInstance", element.Type, softwareSystemInstanceContext.Start.Line, softwareSystemInstanceContext.Start.Column);
                    var softwareSystemInstance = this.VisitSoftwareSystemInstance(softwareSystemInstanceContext, element.Identifier);
                    if(groupId != null)
                    {
                        softwareSystemInstance.GroupId = groupId;
                    }
                    if (this.workspace.Model.ElementIdentifierExists(softwareSystemInstance.Identifier))
                    {
                        throw new Exception(
                            string.Format(
                                "Duplicate softwareSystemInstance identifier '{0}' at {1}:{2}",
                                softwareSystemInstance.Identifier,
                                softwareSystemInstanceContext.Start.Line,
                                softwareSystemInstanceContext.Start.Column));
                    }
                    //softwareSystemInstance.DeploymentEnvironmentIdentifier = element.DeploymentEnvironmentIdentifier;
                    this.workspace.Model.AddModelElement(softwareSystemInstance);
                }

                if (childContext is ContainerInstanceContext containerInstanceContext)
                {
                    this.CheckModelElementBodyProperty("ContainerInstance", element.Type, containerInstanceContext.Start.Line, containerInstanceContext.Start.Column);
                    var containerInstance = this.VisitContainerInstance(containerInstanceContext, element.Identifier);
                    if (groupId != null) { 
                        containerInstance.GroupId = groupId;
                    }
                    if (this.workspace.Model.ElementIdentifierExists(containerInstance.Identifier))
                    {
                        throw new Exception(
                            string.Format(
                                "Duplicate softwareSystemInstance identifier '{0}' at {1}:{2}",
                                containerInstance.Identifier,
                                containerInstanceContext.Start.Line,
                                containerInstanceContext.Start.Column));
                    }
                    containerInstance.DeploymentEnvironmentIdentifier = element.DeploymentEnvironmentIdentifier;
                    this.workspace.Model.AddModelElement(containerInstance);
                }

                if (childContext is InstancesBlockContext instancesBlockContext) { 
                    this.CheckModelElementBodyProperty("Instances", element.Type, instancesBlockContext.Start.Line, instancesBlockContext.Start.Column);
                    element.Instances = this.VisitInstancesBlocks(new InstancesBlockContext[] { instancesBlockContext });
                }

                if (childContext is HealthCheckBlockContext healthCheckBlocksContext)
                {
                    this.CheckModelElementBodyProperty("HealthCheck", element.Type, healthCheckBlocksContext.Start.Line, healthCheckBlocksContext.Start.Column);
                    element.HealthCheck = this.VisitHealthCheckBlocks(new HealthCheckBlockContext[] { healthCheckBlocksContext });
                }

            }

            return element;
        }
    }
}
