using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitDynamicViewTests : BaseTests
    {
        [Test]
        public void WhenDefineDynamicViewThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"dynamic systemId viewId ""view description"" 
            {
                title ""Great system title""
                properties {
                    ""key1"" ""value1""
                    ""key2"" ""value2""
                }
                sourceSystem -> targetSystem ""my description"" ""HTTPS""
                
                
            }");
            var context = parser.dynamicView();
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
            var result = visitor.VisitDynamicView(context);
            // Assert
            Assert.That(result.ElementIdentifier, Is.Not.Null);
            Assert.That(result.ElementIdentifier, Is.EqualTo("systemId"));
            Assert.That(result.Identifier, Is.Not.Null);
            Assert.That(result.Identifier, Is.EqualTo("viewId"));
            Assert.That(result.Description, Is.EqualTo("view description"));
            Assert.That(result.Title, Is.EqualTo("Great system title"));
            Assert.That(result.Properties.Count, Is.EqualTo(2));
            Assert.That(result.Properties["key1"], Is.EqualTo("value1"));
            Assert.That(result.Properties["key2"], Is.EqualTo("value2"));
            Assert.That(result.Default, Is.False);
            Assert.That(result.Relationships.First().Identifier, Is.Not.Null);
            Assert.That(result.Relationships.First().Order, Is.EqualTo(1));
            Assert.That(result.Relationships.First().Source, Is.EqualTo("sourceSystem"));
            Assert.That(result.Relationships.First().Destination, Is.EqualTo("targetSystem"));
            Assert.That(result.Relationships.First().Description, Is.EqualTo("my description"));
            Assert.That(result.Relationships.First().Technology, Is.EqualTo("HTTPS"));
        }
    }
}
