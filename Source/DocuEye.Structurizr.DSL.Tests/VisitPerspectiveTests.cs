using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitPerspectiveTests : BaseTests
    {
        [Test]
        public void WhenDefinePerspectiveThenAllValuesAreOk()
        {
            // Arrange
            var parser = CreateParserFromText(@"perspectiveName ""Description"" perspectiveValue
            ");
            var context = parser.perspective();
            var visitor = new DSLVisitor();
            // Act
            var data = (StructurizrPerspective)visitor.VisitPerspective(context);
            // Assert
            Assert.That(data.Name, Is.EqualTo("perspectiveName"));
            Assert.That(data.Description, Is.EqualTo("Description"));
            Assert.That(data.Value, Is.EqualTo("perspectiveValue"));
        }
    }
}
