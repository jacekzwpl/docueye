using Antlr4.Runtime.Misc;
using DocuEye.Structurizr.DSL.Model;
using static StructurizrDSLParser;

namespace DocuEye.Structurizr.DSL
{
    public partial class DSLVisitor
    {
        public override object VisitModel([NotNull] ModelContext context)
        {
            if(this.workspace.Model == null)
            {
                this.workspace.Model = new StructurizrModel();
            }
            var modelBodyContext = context.modelBody();
            return this.VisitModelBody(modelBodyContext);
        }

        public override object VisitModelBody([NotNull] ModelBodyContext context)
        {
       
            var identifiersKeyword = context.identifiersKeyword();
            if (identifiersKeyword.Length > 1)
            {
                var duplicate = identifiersKeyword[identifiersKeyword.Length - 1];
                throw new Exception(
                    string.Format(
                        "Duplicate identifiers keyword for model at {0}:{1}",
                        duplicate.Start.Line,
                        duplicate.Start.Column));
            }
            if (identifiersKeyword.Length > 0)
            {
                var identifiersValue = identifiersKeyword[0].identifiersValue().GetText();
                if (identifiersValue != "flat" && identifiersValue != "hierarchical")
                {
                    throw new Exception(
                        string.Format(
                            "Invalid identifiers value '{0}' at {1}:{2}",
                            identifiersValue,
                            identifiersKeyword[0].Start.Line,
                            identifiersKeyword[0].Start.Column));
                }
                this.workspace.Model.Identifiers = identifiersValue;

            }

            var propertiesBlocks = context.properties();
            this.workspace.Model.Properties = this.VisitPropertiesBlocks(propertiesBlocks);

            if (context.children == null) { 
                return this.workspace.Model;
            }
            foreach (var childContext in context.children)
            {
                if (childContext is PersonContext)
                {
                    var personContext = (PersonContext)childContext;
                    var person = (StructurizrPerson)this.VisitPerson(personContext);
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
                
                if(childContext is SoftwareSystemContext)
                {
                    var softwareSystemContext = (SoftwareSystemContext)childContext;
                    var softwareSystem = (StructurizrSoftwareSystem)this.VisitSoftwareSystem(softwareSystemContext);
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

                if(childContext is ElementContext)
                {
                    var elementContext = (ElementContext)childContext;
                    var element = (StructurizrCustomElement)this.VisitElement(elementContext);
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

                if(childContext is DeploymentEnvironmentContext)
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

                if(childContext is RelationshipContext)
                {
                    var relationshipContext = (RelationshipContext)childContext;
                    var relationship = (StructurizrRelationship)this.VisitRelationship(relationshipContext, null);
                    this.workspace.Model.AddRelationship(relationship);
                }

                if(childContext is ModelGroupContext)
                {
                    var modelGroupContext = (ModelGroupContext)childContext;
                    var group = this.VisitModelGroup(modelGroupContext, null);
                    this.workspace.Model.Groups.Add(group);
                }

                if(childContext is IncludeFileContext)
                {
                    var includeFileContext = (IncludeFileContext)childContext;
                    this.VisitIncludeFileModelBody(includeFileContext);
                }

                if(childContext is ModelElementReferenceContext)
                {
                    var elementReferenceContext = (ModelElementReferenceContext)childContext;
                    this.VisitModelElementReference(elementReferenceContext);
                }
            }

            this.ResolveImpliedRelationships();
            this.ResolveInstancesRelationships();

            return this.workspace.Model;
        }

        private void ResolveInstancesRelationships()
        {
            foreach (var instanceElement in this.workspace.Model.Elements.Where(o => o.Type == StructurizrModelElementType.SoftwareSystemInstance || o.Type == StructurizrModelElementType.ContainerInstance))
            {
                var relationships = this.workspace.Model.Relationships
                    .Where(o =>o.SourceIdentifier == instanceElement.InstanceOfIdentifier)
                    .ToArray();
                foreach(var relationship in relationships)
                {
                    var destination = this.workspace.Model.Elements
                        .Where(o => o.Identifier == relationship.DestinationIdentifier)
                        .FirstOrDefault();
                    if (destination == null)
                    {
                        continue;
                    }
                    var destinationInstances = this.workspace.Model.Elements
                        .Where(o => o.InstanceOfIdentifier == destination.Identifier)
                        .ToArray();
                    foreach(var destinationInstance in destinationInstances)
                    {
                        if (this.workspace.Model.Relationships.Where(
                            o => o.SourceIdentifier == instanceElement.Identifier
                            && o.DestinationIdentifier == destinationInstance.Identifier).Any())
                        {
                            return;
                        }


                        var instanceRelationship = new StructurizrRelationship(
                            Guid.NewGuid().ToString(), relationship.Tags.ToArray())
                        {
                            Description = relationship.Description,
                            DestinationIdentifier = destinationInstance.Identifier,
                            SourceIdentifier = instanceElement.Identifier,
                            Technology = relationship.Technology,
                            Url = relationship.Url,
                            Properties = relationship.Properties,
                            Perspectives = relationship.Perspectives,
                            LinkedRelationshipIdentifier = relationship.LinkedRelationshipIdentifier == null ? relationship.Identifier : relationship.LinkedRelationshipIdentifier,
                            LinkedRelationshipModelId = relationship.LinkedRelationshipModelId == null ? relationship.ModelId : relationship.LinkedRelationshipModelId
                        };
                        this.workspace.Model.AddRelationship(instanceRelationship);
                    }
                }
            }
        }

        private void ResolveImpliedRelationships() { 

            var elements = this.workspace.Model.Elements
                .Where(o => o.Type == StructurizrModelElementType.Person ||
                            o.Type == StructurizrModelElementType.CustomElement ||
                            o.Type == StructurizrModelElementType.SoftwareSystem || 
                            o.Type == StructurizrModelElementType.Container || 
                            o.Type == StructurizrModelElementType.Component)
                .ToArray();

            foreach (var element in elements)
            {

                var relationships = this.workspace.Model.Relationships
                    .Where(o => 
                        o.SourceIdentifier == element.Identifier && 
                        o.LinkedRelationshipIdentifier == null).ToArray();
                
                if(relationships.Count() == 0)
                {
                    continue;
                }

                var sourceParentIdentifiers = GetParentIdetifiers(element.Identifier);

                foreach (var relationship in relationships)
                {
                    var destination = this.workspace.Model.Elements.Where(o => o.Identifier == relationship.DestinationIdentifier).FirstOrDefault();
                    if(destination == null)
                    {
                        continue;
                    }

                    var destinationParentIdentifiers = GetParentIdetifiers(destination.Identifier);

                    foreach(var destinationParentIdentifier in destinationParentIdentifiers)
                    {
                        this.AddImpliedRelationship(relationship,
                                element.Identifier,
                                destinationParentIdentifier);
                    }

                    foreach(var sourceParentIdentifier in sourceParentIdentifiers)
                    {
                        this.AddImpliedRelationship(relationship,
                                sourceParentIdentifier,
                                destination.Identifier);
                        foreach (var destinationParentIdentifier in destinationParentIdentifiers)
                        {
                            this.AddImpliedRelationship(relationship,
                                sourceParentIdentifier,
                                destinationParentIdentifier);
                        }
                    }
                }
            }
        }

        private IEnumerable<string> GetParentIdetifiers(string elementIdentifier)
        {
            var parentIdentifiers = new List<string>();
            var element = this.workspace.Model.Elements.Where(o => o.Identifier == elementIdentifier).FirstOrDefault();
            if(element == null)
            {
                return new List<string>().ToArray();
            }

            if(element.ParentIdentifier == null)
            {
                return new List<string>().ToArray();
            }
            parentIdentifiers.Add(element.ParentIdentifier);
            parentIdentifiers.AddRange(GetParentIdetifiers(element.ParentIdentifier));
            return parentIdentifiers;
        }

        private void AddImpliedRelationship(StructurizrRelationship linkedRelationship, string sourceIdentifier, string destinationIdentifier)
        {
            if (sourceIdentifier == destinationIdentifier)
            {
                return;
            }
            if (this.workspace.Model.Relationships.Where(
                o => o.SourceIdentifier == sourceIdentifier 
                && o.DestinationIdentifier == destinationIdentifier).Any())
            {
                return;
            }

            var impliedRelationship = new StructurizrRelationship(
                                Guid.NewGuid().ToString(), linkedRelationship.Tags.ToArray())
            {
                Description = linkedRelationship.Description,
                DestinationIdentifier = destinationIdentifier,
                SourceIdentifier = sourceIdentifier,
                Technology = linkedRelationship.Technology,
                Url = linkedRelationship.Url,
                Properties = linkedRelationship.Properties,
                Perspectives = linkedRelationship.Perspectives,
                LinkedRelationshipIdentifier = linkedRelationship.Identifier,
                LinkedRelationshipModelId = linkedRelationship.ModelId

            };
            this.workspace.Model.AddRelationship(impliedRelationship);
        }
    }
}
