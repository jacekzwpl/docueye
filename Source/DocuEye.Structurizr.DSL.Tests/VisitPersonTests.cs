using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitPersonTests : BaseTests
    {
        [Test]
        public void WhenDefinePersonThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            person ""Alice"" ""Alice is a person"" ""tag1,tag2""
            ");
            var context = parser.person();
            var visitor = new DSLVisitor();

            // Act
            var person = (StructurizrPerson)visitor.VisitPerson(context);

            // Assert
            Assert.That(person.Name, Is.EqualTo("Alice"));
            Assert.That(person.Description, Is.EqualTo("Alice is a person"));
            Assert.That(person.Tags, Is.EquivalentTo(new[] { "Person", "tag1", "tag2" }));
            Assert.That(person.Identifier, Is.Not.Null);
        }

        [Test]
        public void WhenDefinePersonWithNameWithOutQuotationMarksThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            person Alice ""Alice is a person"" ""tag1,tag2""
            ");
            var context = parser.person();
            var visitor = new DSLVisitor();

            // Act
            var person = (StructurizrPerson)visitor.VisitPerson(context);

            // Assert
            Assert.That(person.Name, Is.EqualTo("Alice"));
            Assert.That(person.Description, Is.EqualTo("Alice is a person"));
            Assert.That(person.Tags, Is.EquivalentTo(new[] { "Person", "tag1", "tag2" }));
            Assert.That(person.Identifier, Is.Not.Null);
        }

        [Test]
        public void WhenDefinePersonWithIdentifierAndNameWithOutQuoatationMarksThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            alice = person Alice ""Alice is a person"" ""tag1,tag2""
            ");
            var context = parser.person();
            var visitor = new DSLVisitor();

            // Act
            var person = (StructurizrPerson)visitor.VisitPerson(context);

            // Assert
            Assert.That(person.Name, Is.EqualTo("Alice"));
            Assert.That(person.Description, Is.EqualTo("Alice is a person"));
            Assert.That(person.Tags, Is.EquivalentTo(new[] { "Person", "tag1", "tag2" }));
            Assert.That(person.Identifier, Is.EqualTo("alice"));
        }

        [Test]
        public void WhenDefinePersonWithIdentifierThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            alice = person ""Alice"" ""Alice is a person"" ""tag1,tag2""
            ");
            var context = parser.person();
            var visitor = new DSLVisitor();

            // Act
            var person = (StructurizrPerson)visitor.VisitPerson(context);

            // Assert
            Assert.That(person.Name, Is.EqualTo("Alice"));
            Assert.That(person.Description, Is.EqualTo("Alice is a person"));
            Assert.That(person.Tags, Is.EquivalentTo(new[] { "Person", "tag1", "tag2" }));
            Assert.That(person.Identifier, Is.EqualTo("alice"));
        }

        [Test]
        public void WhenPersonNameIsNotSetThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            person
            ");
            var context = parser.person();
            var visitor = new DSLVisitor();

            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitPerson(context));
        }

        [Test]
        public void WhenDefinePersonWithBodyThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"person Alice {
                description ""Alice is a person""
                tags ""tag1""
                properties {
                    ""key1"" ""value1""
                    ""key2"" ""value2""
                }
                url https://docueye.com
            }
            ");
            var context = parser.person();
            var visitor = new DSLVisitor();
            // Act
            var person = (StructurizrPerson)visitor.VisitPerson(context);
            // Assert
            Assert.That(person.Name, Is.EqualTo("Alice"));
            Assert.That(person.Description, Is.EqualTo("Alice is a person"));
            Assert.That(person.Tags, Is.EquivalentTo(new[] { "Person", "tag1" }));
            Assert.That(person.Identifier, Is.Not.Null);
            Assert.That(person.Properties.Count, Is.EqualTo(2));
            Assert.That(person.Properties["key1"], Is.EqualTo("value1"));
            Assert.That(person.Properties["key2"], Is.EqualTo("value2"));
            Assert.That(person.Url, Is.EqualTo("https://docueye.com"));
        }

        [Test]
        public void WhenDefinePersonWithRelationshipsThenRelationshipsExists()
        {
            // Arrange
            var parser = CreateParserFromText(@"alice = person Alice {
               -> bob ""description"" ""technology"" ""tag1,tag2""
            }
            ");
            var context = parser.person();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Elements = new List<StructurizrModelElement>()
                {
                    new StructurizrModelElement("1", "bob")
                }
            };
            // Act
            visitor.VisitPerson(context);
            // Assert
            Assert.That(visitor.Workspace.Model.Relationships.Count, Is.EqualTo(1));
        }
    }
}
