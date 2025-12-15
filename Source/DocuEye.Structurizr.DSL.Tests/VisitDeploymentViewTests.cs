using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitDeploymentViewTests : BaseTests
    {
        [Test]
        public void WhenDefineDeploymentViewThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"deployment systemId prod viewId ""view description"" 
            {
                title ""Great system title""
                include mydidentifier1 mydidentifier2
                exclude mydidentifier3 mydidentifier4
                properties {
                    ""key1"" ""value1""
                    ""key2"" ""value2""
                }
                animation {
                    mydidentifier1 mydidentifier2
                    mydidentifier3 mydidentifier4
                }
                
            }");
            var context = parser.deploymentView();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitDeploymentView(context);
            // Assert
            Assert.That(result.ElementIdentifier, Is.Not.Null);
            Assert.That(result.ElementIdentifier, Is.EqualTo("systemId"));
            Assert.That(result.Environment, Is.Not.Null);
            Assert.That(result.Environment, Is.EqualTo("prod"));
            Assert.That(result.Identifier, Is.Not.Null);
            Assert.That(result.Identifier, Is.EqualTo("viewId"));
            Assert.That(result.Description, Is.EqualTo("view description"));
            Assert.That(result.Title, Is.EqualTo("Great system title"));
            Assert.That(result.Include, Is.EquivalentTo(new[] { "mydidentifier1", "mydidentifier2" }));
            Assert.That(result.Exclude, Is.EquivalentTo(new[] { "mydidentifier3", "mydidentifier4" }));
            Assert.That(result.Properties.Count, Is.EqualTo(2));
            Assert.That(result.Properties["key1"], Is.EqualTo("value1"));
            Assert.That(result.Properties["key2"], Is.EqualTo("value2"));
            Assert.That(result.Animation?.Count(), Is.EqualTo(2));
            Assert.That(result.Animation.First(), Is.EquivalentTo(new string[] { "mydidentifier1", "mydidentifier2" }));
            Assert.That(result.Animation.Last(), Is.EquivalentTo(new string[] { "mydidentifier3", "mydidentifier4" }));
            Assert.That(result.Default, Is.False);
        }

        [Test]
        public void WhenDefineDeploymentViewWithWildcardThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"deployment * prod viewId 
            {                
            }");
            var context = parser.deploymentView();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitDeploymentView(context);
            // Assert
            Assert.That(result.ElementIdentifier, Is.Not.Null);
            Assert.That(result.ElementIdentifier, Is.EqualTo("*"));
            Assert.That(result.Environment, Is.Not.Null);
            Assert.That(result.Environment, Is.EqualTo("prod"));
            Assert.That(result.Identifier, Is.Not.Null);
            Assert.That(result.Identifier, Is.EqualTo("viewId"));
        }
    }
}
