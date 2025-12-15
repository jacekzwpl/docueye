using Antlr4.Runtime.Misc;
using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StructurizrDSLParser;

namespace DocuEye.Structurizr.DSL
{
    public partial class DSLVisitor
    {
        public void VisitModelGroups(ModelGroupContext[]? contexts, string? parentGroupId)
        {
            if (contexts == null)
            {
                return;
            }
            foreach (var context in contexts)
            {
                var group = this.VisitModelGroup(context, parentGroupId);
                this.workspace.Model.Groups.Add(group);
            }
        }

        public StructurizrGroup VisitModelGroup([NotNull] ModelGroupContext context, string? parentGroupId)
        {
            var nameContext = context.name();
            if(nameContext == null)
            {
                throw new Exception(
                    string.Format(
                        "Group name is required at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }
            var name = nameContext.GetText().Trim('"');
            var group = new StructurizrGroup(Guid.NewGuid().ToString(), name, parentGroupId);
            this.VisitModelGroupBody(context.modelGroupBody(), group.Id);

            return group;
        }

        public void VisitModelGroupBody([NotNull] ModelGroupBodyContext context, string groupId)
        {

            if (context.children == null)
            {
                return;
            }

            foreach (var childContext in context.children)
            {
                if (childContext is PersonContext)
                {
                    var personContext = (PersonContext)childContext;
                    var person = (StructurizrPerson)this.VisitPerson(personContext);
                    person.GroupId = groupId;
                    if (this.workspace.Model.ElementIdentifierExists(person.Identifier))
                    {
                        throw new Exception(
                            string.Format(
                                "Duplicate person identifier '{0}' at {1}:{2}",
                                person.Identifier,
                                personContext.Start.Line,
                                personContext.Start.Column));
                    }
                    this.workspace.Model.AddModelElement(person);
                }
                if (childContext is SoftwareSystemContext)
                {
                    var softwareSystemContext = (SoftwareSystemContext)childContext;
                    var softwareSystem = (StructurizrSoftwareSystem)this.VisitSoftwareSystem(softwareSystemContext);
                    softwareSystem.GroupId = groupId;
                    if (this.workspace.Model.ElementIdentifierExists(softwareSystem.Identifier))
                    {
                        throw new Exception(
                            string.Format(
                                "Duplicate software system identifier '{0}' at {1}:{2}",
                                softwareSystem.Identifier,
                                softwareSystemContext.Start.Line,
                                softwareSystemContext.Start.Column));
                    }
                    this.workspace.Model.AddModelElement(softwareSystem);
                }

                if (childContext is ElementContext)
                {
                    var elementContext = (ElementContext)childContext;
                    var element = (StructurizrCustomElement)this.VisitElement(elementContext);
                    element.GroupId = groupId;
                    if (this.workspace.Model.ElementIdentifierExists(element.Identifier))
                    {
                        throw new Exception(
                            string.Format(
                                "Duplicate element identifier '{0}' at {1}:{2}",
                                element.Identifier,
                                elementContext.Start.Line,
                                elementContext.Start.Column));
                    }
                    this.workspace.Model.AddModelElement(element);
                }

                if (childContext is DeploymentEnvironmentContext)
                {
                    var deploymentEnvironmentContext = (DeploymentEnvironmentContext)childContext;
                    var deploymentEnvironment = (StructurizrDeploymentEnvironment)this.VisitDeploymentEnvironment(deploymentEnvironmentContext);
                    if (this.workspace.Model.DeploymentEnvironmentIdentifierExists(deploymentEnvironment.Identifier))
                    {
                        throw new Exception(
                            string.Format(
                                "Duplicate deployment environment identifier '{0}' at {1}:{2}",
                                deploymentEnvironment.Identifier,
                                deploymentEnvironmentContext.Start.Line,
                                deploymentEnvironmentContext.Start.Column));
                    }
                    this.workspace.Model.DeploymentEnvironments.Add(deploymentEnvironment);
                }

                if (childContext is RelationshipContext)
                {
                    var relationshipContext = (RelationshipContext)childContext;
                    var relationship = (StructurizrRelationship)this.VisitRelationship(relationshipContext, null);
                    this.workspace.Model.AddRelationship(relationship);
                }

                if (childContext is ModelGroupContext)
                {
                    var modelGroupContext = (ModelGroupContext)childContext;
                    var group = this.VisitModelGroup(modelGroupContext, groupId);
                    this.workspace.Model.Groups.Add(group);
                }

                if (childContext is IncludeFileContext)
                {
                    var includeFileContext = (IncludeFileContext)childContext;
                    this.VisitIncludeFileModelGroupBody(includeFileContext, groupId);
                }
            }


            /*
            var people = context.person();
            foreach (var personContext in people)
            {
                var person = (StructurizrPerson)this.VisitPerson(personContext);
                person.GroupId = groupId;
                if (this.workspace.Model.ElementIdentifierExists(person.Identifier))
                {
                    throw new Exception(
                        string.Format(
                            "Duplicate person identifier '{0}' at {1}:{2}",
                            person.Identifier,
                            personContext.Start.Line,
                            personContext.Start.Column));
                }
                this.workspace.Model.AddModelElement(person);
            }
            
            var softwareSystems = context.softwareSystem();
            foreach (var softwareSystemContext in softwareSystems)
            {
                var softwareSystem = (StructurizrSoftwareSystem)this.VisitSoftwareSystem(softwareSystemContext);
                softwareSystem.GroupId = groupId;
                if (this.workspace.Model.ElementIdentifierExists(softwareSystem.Identifier))
                {
                    throw new Exception(
                        string.Format(
                            "Duplicate software system identifier '{0}' at {1}:{2}",
                            softwareSystem.Identifier,
                            softwareSystemContext.Start.Line,
                            softwareSystemContext.Start.Column));
                }
                this.workspace.Model.AddModelElement(softwareSystem);
            }
           
            var elementContexts = context.element();
            foreach (var elementContext in elementContexts)
            {
                var element = (StructurizrCustomElement)this.VisitElement(elementContext);
                element.GroupId = groupId;
                if (this.workspace.Model.ElementIdentifierExists(element.Identifier))
                {
                    throw new Exception(
                        string.Format(
                            "Duplicate element identifier '{0}' at {1}:{2}",
                            element.Identifier,
                            elementContext.Start.Line,
                            elementContext.Start.Column));
                }
                this.workspace.Model.AddModelElement(element);
            }

            var deploymentEnvironmentContexts = context.deploymentEnvironment();
            foreach (var deploymentEnvironmentContext in deploymentEnvironmentContexts)
            {
                var deploymentEnvironment = (StructurizrDeploymentEnvironment)this.VisitDeploymentEnvironment(deploymentEnvironmentContext);
                if (this.workspace.Model.DeploymentEnvironmentIdentifierExists(deploymentEnvironment.Identifier))
                {
                    throw new Exception(
                        string.Format(
                            "Duplicate deployment environment identifier '{0}' at {1}:{2}",
                            deploymentEnvironment.Identifier,
                            deploymentEnvironmentContext.Start.Line,
                            deploymentEnvironmentContext.Start.Column));
                }
                this.workspace.Model.DeploymentEnvironments.Add(deploymentEnvironment);
            }

            var modelGroups = context.modelGroup();
            this.VisitModelGroups(modelGroups, groupId);
             */
        }
    }
}
