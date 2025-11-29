using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitRelationshipTests : BaseTests
    {
        [Test]
        public void WhenDefineRelationshipThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            source -> destination ""description"" ""technology"" ""tag1,tag2""
            ");
            var context = parser.relationship();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Elements = new List<StructurizrModelElement>()
                {
                    new StructurizrModelElement("1", "source"),
                    new StructurizrModelElement("1", "destination")
                }
            };

            // Act
            var relationship = (StructurizrRelationship)visitor.VisitRelationship(context, null);

            // Assert
            Assert.That(relationship.SourceIdentifier, Is.EqualTo("source"));
            Assert.That(relationship.Description, Is.EqualTo("description"));
            Assert.That(relationship.DestinationIdentifier, Is.EqualTo("destination"));
            Assert.That(relationship.Technology, Is.EqualTo("technology"));
            Assert.That(relationship.Tags, Is.EquivalentTo(new[] { "Relationship", "tag1", "tag2" }));
            Assert.That(relationship.Identifier, Is.Not.Null);
        }

        [Test]
        public void WhenDefineRelationshipWithIdentifierThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            myrelation = source -> destination ""description"" ""technology"" ""tag1,tag2""
            ");
            var context = parser.relationship();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Elements = new List<StructurizrModelElement>()
                {
                    new StructurizrModelElement("1", "source"),
                    new StructurizrModelElement("1", "destination")
                }
            };

            // Act
            var relationship = (StructurizrRelationship)visitor.VisitRelationship(context, null);

            // Assert
            Assert.That(relationship.SourceIdentifier, Is.EqualTo("source"));
            Assert.That(relationship.Description, Is.EqualTo("description"));
            Assert.That(relationship.DestinationIdentifier, Is.EqualTo("destination"));
            Assert.That(relationship.Technology, Is.EqualTo("technology"));
            Assert.That(relationship.Tags, Is.EquivalentTo(new[] { "Relationship", "tag1", "tag2" }));
            Assert.That(relationship.Identifier, Is.EqualTo("myrelation"));
        }

        [Test]
        public void WhenDefineRelationshipWithNoSourceWithOutParentThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            -> destination ""description"" ""technology"" ""tag1,tag2""
            ");
            var context = parser.relationship();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Elements = new List<StructurizrModelElement>()
                {
                    new StructurizrModelElement("1", "destination")
                }
            };
            // Act & Assert
            var exception = Assert.Throws<Exception>(() => visitor.VisitRelationship(context, null));
            Assert.That(exception?.Message, Is.EqualTo("Source identifier is required for relationship at 1:0"));
        }

        [Test]
        public void WhenDefineRelationshipWithThisSourceWithOutParentThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            this -> destination ""description"" ""technology"" ""tag1,tag2""
            ");
            var context = parser.relationship();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Elements = new List<StructurizrModelElement>()
                {
                    new StructurizrModelElement("1", "destination")
                }
            };
            // Act & Assert
            var exception = Assert.Throws<Exception>(() => visitor.VisitRelationship(context, null));
            Assert.That(exception?.Message, Is.EqualTo("Source identifier is required for relationship at 1:0"));
        }

        [Test]
        public void WhenDefineRelationshipWithThisSourceWithParentThenRelationshipHasSourceIdentifier()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            this -> destination ""description"" ""technology"" ""tag1,tag2""
            ");
            var context = parser.relationship();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Elements = new List<StructurizrModelElement>()
                {
                    new StructurizrModelElement("1", "destination")
                }
            };
            // Act
            var relationship = (StructurizrRelationship)visitor.VisitRelationship(context, "parent");
            // Assert
            Assert.That(relationship.SourceIdentifier, Is.EqualTo("parent"));
        }
        [Test]
        public void WhenDefineRelationshipWithNoSourceWithParentThenRelationshipHasSourceIdentifier()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            -> destination ""description"" ""technology"" ""tag1,tag2""
            ");
            var context = parser.relationship();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Elements = new List<StructurizrModelElement>()
                {
                    new StructurizrModelElement("1", "destination")
                }
            };
            // Act
            var relationship = (StructurizrRelationship)visitor.VisitRelationship(context, "parent");
            // Assert
            Assert.That(relationship.SourceIdentifier, Is.EqualTo("parent"));
        }

        [Test]
        public void WhenDefinedRelationshipWithSourceThatDoesNotExistsThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            source -> destination ""description"" ""technology"" ""tag1,tag2""
            ");
            var context = parser.relationship();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Elements = new List<StructurizrModelElement>()
                {
                    new StructurizrModelElement("1", "destination")
                }
            };
            // Act & Assert
            var exception = Assert.Throws<Exception>(() => visitor.VisitRelationship(context, null));
            Assert.That(exception?.Message, Is.EqualTo("Source identifier 'source' does not exists at 1:0"));
        }

        [Test]
        public void WhenDefinedRelationshipWithDestinationThatDoesNotExistsThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            source -> destination ""description"" ""technology"" ""tag1,tag2""
            ");
            var context = parser.relationship();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Elements = new List<StructurizrModelElement>()
                {
                    new StructurizrModelElement("1", "source")
                }
            };
            // Act & Assert
            var exception = Assert.Throws<Exception>(() => visitor.VisitRelationship(context, null));
            Assert.That(exception?.Message, Is.EqualTo("Destination identifier 'destination' does not exists at 1:0"));
        }

        [Test]
        public void WhenDefinedRelationshipWithSourceThatIsParentThenSourceIdentifierIsSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            parent -> destination ""description"" ""technology"" ""tag1,tag2""
            ");
            var context = parser.relationship();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Elements = new List<StructurizrModelElement>()
                {
                    new StructurizrModelElement("1", "destination")
                }
            };
            // Act
            var relationship = (StructurizrRelationship)visitor.VisitRelationship(context, "parent");
            // Assert
            Assert.That(relationship.SourceIdentifier, Is.EqualTo("parent"));
        }

        [Test]
        public void WhenDefineRelationshipWithBodyThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"source -> destination {
                description ""description""
                tags ""tag1""
                technology ""technology""
                properties {
                    ""key1"" ""value1""
                    ""key2"" ""value2""
                }
                url https://docueye.com
            }
            ");
            var context = parser.relationship();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Elements = new List<StructurizrModelElement>()
                {
                    new StructurizrModelElement("1", "source"),
                    new StructurizrModelElement("1", "destination")
                }
            };
            // Act
            var relationship = (StructurizrRelationship)visitor.VisitRelationship(context, null);
            // Assert
            Assert.That(relationship.SourceIdentifier, Is.EqualTo("source"));
            Assert.That(relationship.DestinationIdentifier, Is.EqualTo("destination"));
            Assert.That(relationship.Technology, Is.EqualTo("technology"));
            Assert.That(relationship.Description, Is.EqualTo("description"));
            Assert.That(relationship.Tags, Is.EquivalentTo(new[] { "Relationship", "tag1" }));
            Assert.That(relationship.Identifier, Is.Not.Null);
            Assert.That(relationship.Properties.Count, Is.EqualTo(2));
            Assert.That(relationship.Properties["key1"], Is.EqualTo("value1"));
            Assert.That(relationship.Properties["key2"], Is.EqualTo("value2"));
            Assert.That(relationship.Url, Is.EqualTo("https://docueye.com"));
        }
    }
}
