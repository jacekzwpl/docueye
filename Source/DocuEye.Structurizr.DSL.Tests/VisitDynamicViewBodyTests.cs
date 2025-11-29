using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitDynamicViewBodyTests : BaseTests
    {
        [Test]
        public void WhenTitleIsSetThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            title ""Great system title""
            ");
            var context = parser.dynamicViewBody();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitDynamicViewBody(context);
            // Assert
            Assert.That(result.Title, Is.EqualTo("Great system title"));
        }
        [Test]
        public void WhenDuplicateTitleIsSetThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            title ""Great system title""
            title ""Great system title""
            ");
            var context = parser.dynamicViewBody();
            var visitor = new DSLVisitor();
            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitDynamicViewBody(context));
        }

        [Test]
        public void WhenPropertiesAreSetThenCorrectValuesAreReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            properties
            {
                ""key1"" ""value1""
                ""key2"" ""value2""
            }
            ");
            var context = parser.dynamicViewBody();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitDynamicViewBody(context);
            // Assert
            Assert.That(result.Properties.Count, Is.EqualTo(2));
            Assert.That(result.Properties["key1"], Is.EqualTo("value1"));
            Assert.That(result.Properties["key2"], Is.EqualTo("value2"));
        }

        [Test]
        public void WhenDefaultIsSetThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            default

            ");
            var context = parser.dynamicViewBody();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitDynamicViewBody(context);
            // Assert
            Assert.That(result.Default, Is.EqualTo(true));
        }

        [Test]
        public void WhenAutolayoutIsDefinedThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            autolayout lr 100 200
            ");
            var context = parser.dynamicViewBody();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitDynamicViewBody(context);
            // Assert
            Assert.That(result.AutomaticLayout?.RankDirection, Is.EqualTo("lr"));
            Assert.That(result.AutomaticLayout?.RankSeparation, Is.EqualTo(100));
            Assert.That(result.AutomaticLayout?.NodeSeparation, Is.EqualTo(200));
        }

        [Test]
        public void WhenRelationshipIsDefinedThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            sourceSystem -> targetSystem ""my description"" ""HTTPS""
            ");
            var context = parser.dynamicViewBody();
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
            var result = visitor.VisitDynamicViewBody(context);

            // Assert
            Assert.That(result.Relationships.First().Identifier, Is.Not.Null);
            Assert.That(result.Relationships.First().Order, Is.EqualTo(1));
            Assert.That(result.Relationships.First().Source, Is.EqualTo("sourceSystem"));
            Assert.That(result.Relationships.First().Destination, Is.EqualTo("targetSystem"));
            Assert.That(result.Relationships.First().Description, Is.EqualTo("my description"));
            Assert.That(result.Relationships.First().Technology, Is.EqualTo("HTTPS"));
        }

        [Test]
        public void WhenRelationshipGroupIsDefinedThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            {
            sourceSystem -> targetSystem ""my description"" ""HTTPS""
            }
            ");
            var context = parser.dynamicViewBody();
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
            var result = visitor.VisitDynamicViewBody(context);

            // Assert
            Assert.That(result.Relationships.First().Identifier, Is.Not.Null);
            Assert.That(result.Relationships.First().Order, Is.EqualTo(1));
            Assert.That(result.Relationships.First().Source, Is.EqualTo("sourceSystem"));
            Assert.That(result.Relationships.First().Destination, Is.EqualTo("targetSystem"));
            Assert.That(result.Relationships.First().Description, Is.EqualTo("my description"));
            Assert.That(result.Relationships.First().Technology, Is.EqualTo("HTTPS"));
        }

    }
}
