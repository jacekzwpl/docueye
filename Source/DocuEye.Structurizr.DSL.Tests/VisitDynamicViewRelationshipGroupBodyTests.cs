using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitDynamicViewRelationshipGroupBodyTests : BaseTests
    {
        [Test]
        public void WhenDefineRelationshipsThenAllDataIsCorrect()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            system1 -> system2 ""my description"" ""HTTPS""
            system2 -> system3 ""my description"" ""HTTPS""
            rel3 ""my description""
            ");
            var context = parser.dynamicViewRelationshipGroupBody();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Relationships = new List<StructurizrRelationship>()
                {
                    new StructurizrRelationship("rel1", new string[] {})
                    {
                        SourceIdentifier = "system1",
                        DestinationIdentifier = "system2"
                    },
                    new StructurizrRelationship("rel2", new string[] {})
                    {
                        SourceIdentifier = "system2",
                        DestinationIdentifier = "system3"
                    },
                    new StructurizrRelationship("rel3", new string[] {})
                    {
                        SourceIdentifier = "system1",
                        DestinationIdentifier = "system3",
                        Technology = "HTTPS"
                    }
                }
            };
            // Act
            var result = visitor.VisitDynamicViewRelationshipGroupBody(context, 1).ToList();
            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(3));
            Assert.That(result.First().Identifier, Is.Not.Null);
            Assert.That(result.First().Order, Is.EqualTo(1));
            Assert.That(result.First().Source, Is.EqualTo("system1"));
            Assert.That(result.First().Destination, Is.EqualTo("system2"));
            Assert.That(result.First().Description, Is.EqualTo("my description"));
            Assert.That(result.First().Technology, Is.EqualTo("HTTPS"));
            Assert.That(result[1].Identifier, Is.Not.Null);
            Assert.That(result[1].Order, Is.EqualTo(2));
            Assert.That(result[1].Source, Is.EqualTo("system2"));
            Assert.That(result[1].Destination, Is.EqualTo("system3"));
            Assert.That(result[1].Description, Is.EqualTo("my description"));
            Assert.That(result[1].Technology, Is.EqualTo("HTTPS"));
            Assert.That(result[2].Identifier, Is.Not.Null);
            Assert.That(result[2].Order, Is.EqualTo(3));
            Assert.That(result[2].Source, Is.EqualTo("system1"));
            Assert.That(result[2].Destination, Is.EqualTo("system3"));
            Assert.That(result[2].Description, Is.EqualTo("my description"));
            Assert.That(result[2].Technology, Is.EqualTo("HTTPS"));
        }

        [Test]
        public void WhenDefineNestedGroupsThenAllDataIsCorrect()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            {
                system1 -> system2 ""my description"" ""HTTPS""
                system2 -> system3 ""my description"" ""HTTPS""
            }
            {
                rel3 ""my description""
            }
            ");
            var context = parser.dynamicViewRelationshipGroupBody();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Relationships = new List<StructurizrRelationship>()
                {
                    new StructurizrRelationship("rel1", new string[] {})
                    {
                        SourceIdentifier = "system1",
                        DestinationIdentifier = "system2"
                    },
                    new StructurizrRelationship("rel2", new string[] {})
                    {
                        SourceIdentifier = "system2",
                        DestinationIdentifier = "system3"
                    },
                    new StructurizrRelationship("rel3", new string[] {})
                    {
                        SourceIdentifier = "system1",
                        DestinationIdentifier = "system3",
                        Technology = "HTTPS"
                    }
                }
            };
            // Act
            var result = visitor.VisitDynamicViewRelationshipGroupBody(context, 1).ToList();
            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(3));
            Assert.That(result.First().Identifier, Is.Not.Null);
            Assert.That(result.First().Order, Is.EqualTo(1));
            Assert.That(result.First().Source, Is.EqualTo("system1"));
            Assert.That(result.First().Destination, Is.EqualTo("system2"));
            Assert.That(result.First().Description, Is.EqualTo("my description"));
            Assert.That(result.First().Technology, Is.EqualTo("HTTPS"));
            Assert.That(result[1].Identifier, Is.Not.Null);
            Assert.That(result[1].Order, Is.EqualTo(2));
            Assert.That(result[1].Source, Is.EqualTo("system2"));
            Assert.That(result[1].Destination, Is.EqualTo("system3"));
            Assert.That(result[1].Description, Is.EqualTo("my description"));
            Assert.That(result[1].Technology, Is.EqualTo("HTTPS"));
            Assert.That(result[2].Identifier, Is.Not.Null);
            Assert.That(result[2].Order, Is.EqualTo(2));
            Assert.That(result[2].Source, Is.EqualTo("system1"));
            Assert.That(result[2].Destination, Is.EqualTo("system3"));
            Assert.That(result[2].Description, Is.EqualTo("my description"));
            Assert.That(result[2].Technology, Is.EqualTo("HTTPS"));
        }
    }
}
