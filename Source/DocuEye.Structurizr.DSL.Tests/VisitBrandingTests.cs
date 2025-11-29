using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitBrandingTests : BaseTests
    {
        [Test]
        public void WhenBrandingIsDefinedThenCorrectValuesAreReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            branding {
                logo ""logo.png""
                font ""Arial""
            }
            ");
            var context = parser.branding();
            var visitor = new DSLVisitor();

            // Act
            var result = visitor.VisitBranding(context);

            // Assert
            Assert.That(result.Logo, Is.EqualTo("logo.png"));
            Assert.That(result.Font, Is.EqualTo("Arial"));
        }
    }
}
