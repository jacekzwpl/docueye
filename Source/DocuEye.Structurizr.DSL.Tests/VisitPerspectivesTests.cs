using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StructurizrDSLParser;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitPerspectivesTests : BaseTests
    {
        [Test]
        public void WhenDefinePerspectivesThenAllValuesAreOk()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            perspectives {
                perspectiveName1 ""Description1"" perspectiveValue1
                perspectiveName2 ""Description2"" perspectiveValue2
            }
            ");
            var context = parser.perspectives();
            var visitor = new DSLVisitor();
            // Act
            var data = (IEnumerable<StructurizrPerspective>?)visitor.VisitPerspectives(new PerspectivesContext[] { context } );
            // Assert
            Assert.That(data?.Count(), Is.EqualTo(2));
            Assert.That(data?.First().Name, Is.EqualTo("perspectiveName1"));
            Assert.That(data?.First().Description, Is.EqualTo("Description1"));
            Assert.That(data?.First().Value, Is.EqualTo("perspectiveValue1"));
            Assert.That(data?.Last().Name, Is.EqualTo("perspectiveName2"));
            Assert.That(data?.Last().Description, Is.EqualTo("Description2"));
            Assert.That(data?.Last().Value, Is.EqualTo("perspectiveValue2"));
        }

        [Test]
        public void WhenDefineMultiplePerspectivesThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            perspectives {
                perspectiveName1 ""Description1"" perspectiveValue1
                perspectiveName2 ""Description2"" perspectiveValue2
            }
            ");
            var visitor = new DSLVisitor();
            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitPerspectives(new PerspectivesContext[] { parser.perspectives(), parser.perspectives() }));
        }

        


    }
}
