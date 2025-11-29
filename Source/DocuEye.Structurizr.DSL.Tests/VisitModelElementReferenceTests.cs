using DocuEye.Structurizr.DSL.Model;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitModelElementReferenceTests : BaseTests
    {

        [Test]
        public void WhenReferenceElementUsingExElementNotationThenElementIsModified()
        {
            // Arrange
            var workspace = new StructurizrWorkspace();
            workspace.Model.AddModelElement(new StructurizrPerson("elementId", new string[] { "Element" }));
            var parser = CreateParserFromText(@"
            !element elementId {
                tags ""tag1,tag2""
            }
            ");
            var context = parser.modelElementReference();
            var visitor = new DSLVisitor(workspace);
            // Act
            var result = visitor.VisitModelElementReference(context);
            // Assert
            Assert.That(result, Is.EqualTo("elementId"));
            Assert.That(
                visitor.Workspace.Model.Elements.SingleOrDefault(o => o.Identifier == "elementId")?.Tags,
                Is.EquivalentTo(new[] { "Element", "tag1", "tag2" }));
        }

        [Test]
        public void WhenReferenceRelationshipUsingExRelationshipNotationThenRelationshipIsModified()
        {
            // Arrange
            var workspace = new StructurizrWorkspace();
            workspace.Model.AddRelationship(new StructurizrRelationship("relationshipId", new string[] { "Relationship" })
            {
                SourceIdentifier = "sourceId",
                DestinationIdentifier = "destinationId",
                LinkedRelationshipIdentifier = "linkedId",
                LinkedRelationshipModelId = "linkedModelId",
                Description = "A relationship"

            });
            var parser = CreateParserFromText(@"
            !relationship relationshipId {
                tags ""tag1,tag2""
            }
            ");
            var context = parser.modelElementReference();
            var visitor = new DSLVisitor(workspace);
            // Act
            var result = visitor.VisitModelElementReference(context);
            // Assert
            var relationship = visitor.Workspace.Model.Relationships.SingleOrDefault(o => o.Identifier == "relationshipId");
            Assert.IsNotNull(relationship);
            Assert.That(relationship.Identifier, Is.EqualTo("relationshipId"));
            Assert.That(relationship.SourceIdentifier, Is.EqualTo("sourceId"));
            Assert.That(relationship.DestinationIdentifier, Is.EqualTo("destinationId"));
            Assert.That(relationship.LinkedRelationshipIdentifier, Is.EqualTo("linkedId"));
            Assert.That(relationship.LinkedRelationshipModelId, Is.EqualTo("linkedModelId"));
            Assert.That(relationship.Description, Is.EqualTo("A relationship"));
            Assert.That(
                relationship?.Tags,
                Is.EquivalentTo(new[] { "Relationship", "tag1", "tag2" }));
        }

        [Test]
        public void WhenReferenceRelationshipThenRelationshipIsModified()
        {
            // Arrange
            var workspace = new StructurizrWorkspace();
            workspace.Model.AddRelationship(new StructurizrRelationship("relationshipId", new string[] { "Relationship" })
            {
                SourceIdentifier = "sourceId",
                DestinationIdentifier = "destinationId",
                LinkedRelationshipIdentifier = "linkedId",
                LinkedRelationshipModelId = "linkedModelId",
                Description = "A relationship"

            });
            var parser = CreateParserFromText(@"
            !ref relationshipId {
                tags ""tag1,tag2""
            }
            ");
            var context = parser.modelElementReference();
            var visitor = new DSLVisitor(workspace);
            // Act
            var result = visitor.VisitModelElementReference(context);
            // Assert
            var relationship = visitor.Workspace.Model.Relationships.SingleOrDefault(o => o.Identifier == "relationshipId");
            Assert.IsNotNull(relationship);
            Assert.That(relationship.Identifier, Is.EqualTo("relationshipId"));
            Assert.That(relationship.SourceIdentifier, Is.EqualTo("sourceId"));
            Assert.That(relationship.DestinationIdentifier, Is.EqualTo("destinationId"));
            Assert.That(relationship.LinkedRelationshipIdentifier, Is.EqualTo("linkedId"));
            Assert.That(relationship.LinkedRelationshipModelId, Is.EqualTo("linkedModelId"));
            Assert.That(relationship.Description, Is.EqualTo("A relationship"));
            Assert.That(
                relationship?.Tags,
                Is.EquivalentTo(new[] { "Relationship", "tag1", "tag2" }));
        }

        [Test]
        public void WhenReferencePersonThenElementIsModified()
        {
            // Arrange
            var workspace = new StructurizrWorkspace();
            workspace.Model.AddModelElement(new StructurizrPerson("elementId", new string[] { "Element" }));
            var parser = CreateParserFromText(@"
            !ref elementId {
                tags ""tag1,tag2""
            }
            ");
            var context = parser.modelElementReference();
            var visitor = new DSLVisitor(workspace);
            // Act
            var result = visitor.VisitModelElementReference(context);
            // Assert
            Assert.That(result, Is.EqualTo("elementId"));
            Assert.That(
                visitor.Workspace.Model.Elements.SingleOrDefault(o => o.Identifier == "elementId")?.Tags, 
                Is.EquivalentTo(new[] { "Element", "tag1", "tag2" }));
        }

        [Test]
        public void WhenReferenceSoftwareSystemThenElementIsModified()
        {
            // Arrange
            var workspace = new StructurizrWorkspace();
            workspace.Model.AddModelElement(new StructurizrSoftwareSystem("elementId", new string[] { "Element" }));
            var parser = CreateParserFromText(@"
            !ref elementId {
                tags ""tag1,tag2""
            }
            ");
            var context = parser.modelElementReference();
            var visitor = new DSLVisitor(workspace);
            // Act
            var result = visitor.VisitModelElementReference(context);
            // Assert
            Assert.That(result, Is.EqualTo("elementId"));
            Assert.That(
                visitor.Workspace.Model.Elements.SingleOrDefault(o => o.Identifier == "elementId")?.Tags,
                Is.EquivalentTo(new[] { "Element", "tag1", "tag2" }));
        }

        [Test]
        public void WhenReferenceContainerThenElementIsModified()
        {
            // Arrange
            var workspace = new StructurizrWorkspace();
            workspace.Model.AddModelElement(new StructurizrContainer("elementId","system", new string[] { "Element" }));
            var parser = CreateParserFromText(@"
            !ref elementId {
                tags ""tag1,tag2""
            }
            ");
            var context = parser.modelElementReference();
            var visitor = new DSLVisitor(workspace);
            // Act
            var result = visitor.VisitModelElementReference(context);
            // Assert
            Assert.That(result, Is.EqualTo("elementId"));
            Assert.That(
                visitor.Workspace.Model.Elements.SingleOrDefault(o => o.Identifier == "elementId")?.Tags,
                Is.EquivalentTo(new[] { "Element", "tag1", "tag2" }));
        }

        [Test]
        public void WhenReferenceComponentThenElementIsModified()
        {
            // Arrange
            var workspace = new StructurizrWorkspace();
            workspace.Model.AddModelElement(new StructurizrComponent("elementId", "container", new string[] { "Element" }));
            var parser = CreateParserFromText(@"
            !ref elementId {
                tags ""tag1,tag2""
            }
            ");
            var context = parser.modelElementReference();
            var visitor = new DSLVisitor(workspace);
            // Act
            var result = visitor.VisitModelElementReference(context);
            // Assert
            Assert.That(result, Is.EqualTo("elementId"));
            Assert.That(
                visitor.Workspace.Model.Elements.SingleOrDefault(o => o.Identifier == "elementId")?.Tags,
                Is.EquivalentTo(new[] { "Element", "tag1", "tag2" }));
        }

        [Test]
        public void WhenReferenceDeploymentNodeThenElementIsModified()
        {
            // Arrange
            var workspace = new StructurizrWorkspace();
            workspace.Model.AddModelElement(new StructurizrDeploymentNode("elementId", "enviromentId", null, new string[] { "Element" }));
            var parser = CreateParserFromText(@"
            !ref elementId {
                tags ""tag1,tag2""
            }
            ");
            var context = parser.modelElementReference();
            var visitor = new DSLVisitor(workspace);
            // Act
            var result = visitor.VisitModelElementReference(context);
            // Assert
            Assert.That(result, Is.EqualTo("elementId"));
            Assert.That(
                visitor.Workspace.Model.Elements.SingleOrDefault(o => o.Identifier == "elementId")?.Tags,
                Is.EquivalentTo(new[] { "Element", "tag1", "tag2" }));
        }

        [Test]
        public void WhenReferenceSoftwareSystemInstanceThenElementIsModified()
        {
            // Arrange
            var workspace = new StructurizrWorkspace();
            workspace.Model.AddModelElement(new StructurizrSoftwareSystem("systemId", new string[] { "Element" }));
            workspace.Model.AddModelElement(new StructurizrSoftwareSystemInstance("elementId", "systemId", "nodeId", new string[] { "Element" }));
            var parser = CreateParserFromText(@"
            !ref elementId {
                tags ""tag1,tag2""
            }
            ");
            var context = parser.modelElementReference();
            var visitor = new DSLVisitor(workspace);
            // Act
            var result = visitor.VisitModelElementReference(context);
            // Assert
            var element = visitor.Workspace.Model.Elements.SingleOrDefault(o => o.Identifier == "elementId");
            Assert.That(element, !Is.Null);
            Assert.That(element.Identifier, Is.EqualTo("elementId"));
            Assert.That(element.InstanceOfIdentifier, Is.EqualTo("systemId"));
            Assert.That(element.ParentIdentifier, Is.EqualTo("nodeId"));
            Assert.That(
                element?.Tags,
                Is.EquivalentTo(new[] { "Element", "tag1", "tag2" }));
        }

        [Test]
        public void WhenReferenceContainerInstanceThenElementIsModified()
        {
            // Arrange
            var workspace = new StructurizrWorkspace();
            workspace.Model.AddModelElement(new StructurizrContainer("containerId", "systemId", new string[] { "Element" }));
            workspace.Model.AddModelElement(new StructurizrContainerInstance("elementId", "containerId", "nodeId", new string[] { "Element" }));
            var parser = CreateParserFromText(@"
            !ref elementId {
                tags ""tag1,tag2""
            }
            ");
            var context = parser.modelElementReference();
            var visitor = new DSLVisitor(workspace);
            // Act
            var result = visitor.VisitModelElementReference(context);
            // Assert
            var element = visitor.Workspace.Model.Elements.SingleOrDefault(o => o.Identifier == "elementId");
            Assert.That(element, !Is.Null);
            Assert.That(element.Identifier, Is.EqualTo("elementId"));
            Assert.That(element.InstanceOfIdentifier, Is.EqualTo("containerId"));
            Assert.That(element.ParentIdentifier, Is.EqualTo("nodeId"));
            Assert.That(
                element?.Tags,
                Is.EquivalentTo(new[] { "Element", "tag1", "tag2" }));
        }

        [Test]
        public void WhenReferenceInfrastructureNodeThenElementIsModified()
        {
            // Arrange
            var workspace = new StructurizrWorkspace();
            workspace.Model.AddModelElement(new StructurizrInfrastructureNode("elementId", "nodeId", new string[] { "Element" }));
            var parser = CreateParserFromText(@"
            !ref elementId {
                tags ""tag1,tag2""
            }
            ");
            var context = parser.modelElementReference();
            var visitor = new DSLVisitor(workspace);
            // Act
            var result = visitor.VisitModelElementReference(context);
            // Assert
            var element = visitor.Workspace.Model.Elements.SingleOrDefault(o => o.Identifier == "elementId");
            Assert.That(element, !Is.Null);
            Assert.That(element.Identifier, Is.EqualTo("elementId"));
            Assert.That(element.ParentIdentifier, Is.EqualTo("nodeId"));
            Assert.That(
                element?.Tags,
                Is.EquivalentTo(new[] { "Element", "tag1", "tag2" }));
        }

        [Test]
        public void WhenReferenceDeploymentEnvironmentThenElementIsModified()
        {
            // Arrange
            var workspace = new StructurizrWorkspace();
            workspace.Model.DeploymentEnvironments.Add(new StructurizrDeploymentEnvironment("enviromentId", "Environment"));
            workspace.Model.AddModelElement(new StructurizrDeploymentNode("nodeId0", "enviromentId", null, new string[] { "Element" }));
            var parser = CreateParserFromText(@"
            !ref enviromentId {
                nodeId1 = deploymentNode ""My New Node"" {
                    tags ""tag1,tag2""
                }
            }
            ");
            var context = parser.modelElementReference();
            var visitor = new DSLVisitor(workspace);
            // Act
            var result = visitor.VisitModelElementReference(context);
            // Assert

            Assert.That(result, Is.EqualTo("enviromentId"));
            Assert.That(
                visitor.Workspace.Model.DeploymentEnvironments.SingleOrDefault(o => o.Identifier == "enviromentId")?.Name,
                Is.EqualTo("Environment"));
            Assert.That(visitor.Workspace.Model.Elements.Count(o => o.DeploymentEnvironmentIdentifier == "enviromentId"), Is.EqualTo(2));
        }

        [Test]
        public void WhenReferenceNonExistingElementThenExceptionIsThrown()
        {
            // Arrange
            var workspace = new StructurizrWorkspace();
            var parser = CreateParserFromText(@"
            !ref personId {
                tags ""tag1,tag2""
            }
            ");
            var context = parser.modelElementReference();
            var visitor = new DSLVisitor(workspace);

            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitModelElementReference(context));


        }
    }
}
