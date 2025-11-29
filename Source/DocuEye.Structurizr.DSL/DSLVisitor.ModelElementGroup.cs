using Antlr4.Runtime.Misc;
using DocuEye.Structurizr.DSL.Model;
using static StructurizrDSLParser;

namespace DocuEye.Structurizr.DSL
{
    public partial class DSLVisitor
    {
        public StructurizrGroup VisitModelElementGroup([NotNull] ModelElementGroupContext context, StructurizrModelElement parentElement, string? parentGroupId)
        {
            var nameContext = context.name();
            if (nameContext == null)
            {
                throw new Exception(
                    string.Format(
                        "Group name is required at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }
            var name = nameContext.GetText().Trim('"');
            var group = new StructurizrGroup(Guid.NewGuid().ToString(), name, parentGroupId);
            this.VisitModelElementGroupBody(context.modelElementGroupBody(), parentElement, group.Id);
            return group;
        }

        

        public void VisitModelElementGroupBody([NotNull] ModelElementGroupBodyContext context, StructurizrModelElement element, string groupId)
        {
            if (context.children == null)
            {
                return;
            }
            foreach (var childContext in context.children)
            {
                if (childContext is ContainerContext containerContext)
                {
                    this.CheckModelElementBodyProperty("Container", element.Type, containerContext.Start.Line, containerContext.Start.Column);
                    var container = this.VisitContainer(containerContext, element.Identifier);
                    container.GroupId = groupId;
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

                if (childContext is ComponentContext componentContext)
                {
                    this.CheckModelElementBodyProperty("Component", element.Type, componentContext.Start.Line, componentContext.Start.Column);
                    var component = this.VisitComponent(componentContext, element.Identifier);
                    component.GroupId = groupId;
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

                if (childContext is ModelElementGroupContext)
                {
                    var groupContext = (ModelElementGroupContext)childContext;
                    var group = this.VisitModelElementGroup(groupContext, element, groupId);
                    this.workspace.Model.Groups.Add(group);
                }

                if (childContext is RelationshipContext)
                {
                    var relationshipContext = (RelationshipContext)childContext;
                    var relationship = (StructurizrRelationship)this.VisitRelationship(relationshipContext, element.Identifier);
                    this.workspace.Model.AddRelationship(relationship);
                }

                if (childContext is IncludeFileContext)
                {
                    var includeFileContext = (IncludeFileContext)childContext;
                    this.VisitIncludeFileModelElementGroupBody(includeFileContext, element, groupId);
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
                    infrastructureNode.GroupId = groupId;
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
                    softwareSystemInstance.GroupId = groupId;
                    if (this.workspace.Model.ElementIdentifierExists(softwareSystemInstance.Identifier))
                    {
                        throw new Exception(
                            string.Format(
                                "Duplicate softwareSystemInstance identifier '{0}' at {1}:{2}",
                                softwareSystemInstance.Identifier,
                                softwareSystemInstanceContext.Start.Line,
                                softwareSystemInstanceContext.Start.Column));
                    }
                    this.workspace.Model.AddModelElement(softwareSystemInstance);
                }

                if (childContext is ContainerInstanceContext containerInstanceContext)
                {
                    this.CheckModelElementBodyProperty("ContainerInstance", element.Type, containerInstanceContext.Start.Line, containerInstanceContext.Start.Column);
                    var containerInstance = this.VisitContainerInstance(containerInstanceContext, element.Identifier);
                    containerInstance.GroupId = groupId;
                    if (this.workspace.Model.ElementIdentifierExists(containerInstance.Identifier))
                    {
                        throw new Exception(
                            string.Format(
                                "Duplicate softwareSystemInstance identifier '{0}' at {1}:{2}",
                                containerInstance.Identifier,
                                containerInstanceContext.Start.Line,
                                containerInstanceContext.Start.Column));
                    }
                    this.workspace.Model.AddModelElement(containerInstance);
                }


            }
        }
    }
}
