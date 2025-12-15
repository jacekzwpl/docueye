using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitStyleBackgroundBlockTests : BaseTests
    {
        [Test]
        public void WhenBlockIsDefindedThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            background #ffffff
            ");
            var context = parser.styleBackgroundBlock();
            var visitor = new DSLVisitor();


            // Act
            var value = (string)visitor.VisitStyleBackgroundBlock(context);

            // Assert
            Assert.That(value, Is.EqualTo("#ffffff"));
        }
    }
}
