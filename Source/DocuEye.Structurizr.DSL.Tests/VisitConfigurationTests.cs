using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitConfigurationTests : BaseTests
    {
        [Test]
        public void WhenConfigurationIsDefinedThenCorrectValuesAreReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            configuration {
                visibility private
                scope none
                users {
                    user1 role1
                }
            }
            ");
            var context = parser.configuration();
            var visitor = new DSLVisitor();

            // Act
            var result = visitor.VisitConfiguration(context);

            // Assert
            Assert.That(result.Visibility, Is.EqualTo("private"));
            Assert.That(result.Scope, Is.EqualTo("none"));
            Assert.That(result.Users?.Count(), Is.EqualTo(1));
            Assert.That(result.Users?.First().Username, Is.EqualTo("user1"));
            Assert.That(result.Users?.First().Role, Is.EqualTo("role1"));
        }
    }
}
