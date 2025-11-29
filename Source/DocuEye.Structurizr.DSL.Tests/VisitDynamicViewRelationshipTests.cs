using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitDynamicViewRelationshipTests : BaseTests
    {
        [Test]
        public void WhenRelationshipIsDefindedWithOrderThenAllValuesAreMatched() {
            // Arrange
            var parser = CreateParserFromText(@"
            1: sourceSystem -> targetSystem ""my description"" ""HTTPS""
            ");
            var context = parser.dynamicViewRelationship();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Relationships = new List<StructurizrRelationship>()
                {
                    new StructurizrRelationship("relationId", new string[] {})
                    {
                        SourceIdentifier = "sourceSystem",
                        DestinationIdentifier = "targetSystem"
                    }
                }
            };
            // Act
            var result = (StructurizrDynamicViewRelationship)visitor.VisitDynamicViewRelationship(context);

            // Assert
            Assert.That(result.Identifier, Is.Not.Null);
            Assert.That(result.Order, Is.EqualTo(1));
            Assert.That(result.Source, Is.EqualTo("sourceSystem"));
            Assert.That(result.Destination, Is.EqualTo("targetSystem"));
            Assert.That(result.Description, Is.EqualTo("my description"));
            Assert.That(result.Technology, Is.EqualTo("HTTPS"));

        }

        [Test]
        public void WhenRelationshipIsDefindedWithoutOrderThenAllValuesAreMatched()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            sourceSystem -> targetSystem ""my description"" ""HTTPS""
            ");
            var context = parser.dynamicViewRelationship();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Relationships = new List<StructurizrRelationship>()
                {
                    new StructurizrRelationship("relationId", new string[] {})
                    {
                        SourceIdentifier = "sourceSystem",
                        DestinationIdentifier = "targetSystem"
                    }
                }
            };
            // Act
            var result = (StructurizrDynamicViewRelationship)visitor.VisitDynamicViewRelationship(context);

            // Assert
            Assert.That(result.Identifier, Is.Not.Null);
            Assert.That(result.Order, Is.EqualTo(0));
            Assert.That(result.Source, Is.EqualTo("sourceSystem"));
            Assert.That(result.Destination, Is.EqualTo("targetSystem"));
            Assert.That(result.Description, Is.EqualTo("my description"));
            Assert.That(result.Technology, Is.EqualTo("HTTPS"));

        }

        [Test]
        public void WhenRelationshipIdetifierIsDefindedWithoutOrderThenAllValuesAreMatched()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            relationId ""my description""
            ");
            var context = parser.dynamicViewRelationship();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Relationships = new List<StructurizrRelationship>()
                {
                    new StructurizrRelationship("relationId", new string[] {})
                    {
                        SourceIdentifier = "sourceSystem",
                        DestinationIdentifier = "targetSystem",
                        Technology = "HTTPS"
                    }
                }
            };
            // Act
            var result = (StructurizrDynamicViewRelationship)visitor.VisitDynamicViewRelationship(context);

            // Assert
            Assert.That(result.Identifier, Is.Not.Null);
            Assert.That(result.Order, Is.EqualTo(0));
            Assert.That(result.Source, Is.EqualTo("sourceSystem"));
            Assert.That(result.Destination, Is.EqualTo("targetSystem"));
            Assert.That(result.Description, Is.EqualTo("my description"));
            Assert.That(result.Technology, Is.EqualTo("HTTPS"));

        }

        [Test]
        public void WhenRelationshipIdetifierIsDefindedWithOrderThenAllValuesAreMatched()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            2: relationId ""my description""
            ");
            var context = parser.dynamicViewRelationship();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Relationships = new List<StructurizrRelationship>()
                {
                    new StructurizrRelationship("relationId", new string[] {})
                    {
                        SourceIdentifier = "sourceSystem",
                        DestinationIdentifier = "targetSystem",
                        Technology = "HTTPS"
                    }
                }
            };
            // Act
            var result = (StructurizrDynamicViewRelationship)visitor.VisitDynamicViewRelationship(context);

            // Assert
            Assert.That(result.Identifier, Is.Not.Null);
            Assert.That(result.Order, Is.EqualTo(2));
            Assert.That(result.Source, Is.EqualTo("sourceSystem"));
            Assert.That(result.Destination, Is.EqualTo("targetSystem"));
            Assert.That(result.Description, Is.EqualTo("my description"));
            Assert.That(result.Technology, Is.EqualTo("HTTPS"));

        }

        [Test]
        public void WhenRelationshipIsDefindedButDoNotExistsInModelThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            sourceSystem -> targetSystem ""my description"" ""HTTPS""
            ");
            var context = parser.dynamicViewRelationship();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Relationships = new List<StructurizrRelationship>()
            };
            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitDynamicViewRelationship(context));

        }
    }
}
