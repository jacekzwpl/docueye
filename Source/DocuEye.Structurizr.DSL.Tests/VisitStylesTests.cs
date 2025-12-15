using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitStylesTests : BaseTests
    {
        [Test]
        public void WhenStylesAreDefinded_ThenStylesAreReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            styles {
                element tag1 {
                    color #ff0000
                }
                relationship tag2 {
                    color #ff0000
                }
            }
            ");
            var context = parser.styles();
            var visitor = new DSLVisitor();


            // Act
            var value = visitor.VisitStyles(context);

            // Assert
            Assert.That(value.ElementStyles.Count, Is.EqualTo(1));
            Assert.That(value.ElementStyles.First().Tag, Is.EqualTo("tag1"));
            Assert.That(value.ElementStyles.First().Color, Is.EqualTo("#ff0000"));
            Assert.That(value.RelationshipStyles.Count, Is.EqualTo(1));
            Assert.That(value.RelationshipStyles.First().Tag, Is.EqualTo("tag2"));
            Assert.That(value.RelationshipStyles.First().Color, Is.EqualTo("#ff0000"));
        }
    }
}
