using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitFilteredViewTests : BaseTests
    {
        [Test]
        public void WhenDefineFilterdViewWithIncludeModeThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"filtered baseId include ""Tag1,Tag2"" viewId ""view description"" 
            {
                title ""Great system title""
                properties {
                    ""key1"" ""value1""
                    ""key2"" ""value2""
                }
                
            }");
            var context = parser.filteredView();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitFilteredView(context);
            // Assert
            Assert.That(result.BaseViewIdentifier, Is.Not.Null);
            Assert.That(result.BaseViewIdentifier, Is.EqualTo("baseId"));
            Assert.That(result.Identifier, Is.Not.Null);
            Assert.That(result.Identifier, Is.EqualTo("viewId"));
            Assert.That(result.Description, Is.EqualTo("view description"));
            Assert.That(result.Title, Is.EqualTo("Great system title"));
            Assert.That(result.Properties.Count, Is.EqualTo(2));
            Assert.That(result.Properties["key1"], Is.EqualTo("value1"));
            Assert.That(result.Properties["key2"], Is.EqualTo("value2"));
            Assert.That(result.Default, Is.False);
            Assert.That(result.FilterMode, Is.EqualTo("include"));
            Assert.That(result.Tags.Count, Is.EqualTo(2));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Tag1", "Tag2" }));
        }

        [Test]
        public void WhenDefineFilterdViewWithExcludeModeThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"filtered baseId exclude ""Tag1,Tag2"" viewId ""view description"" 
            {
                title ""Great system title""
                properties {
                    ""key1"" ""value1""
                    ""key2"" ""value2""
                }
                
            }");
            var context = parser.filteredView();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitFilteredView(context);
            // Assert
            Assert.That(result.BaseViewIdentifier, Is.Not.Null);
            Assert.That(result.BaseViewIdentifier, Is.EqualTo("baseId"));
            Assert.That(result.Identifier, Is.Not.Null);
            Assert.That(result.Identifier, Is.EqualTo("viewId"));
            Assert.That(result.Description, Is.EqualTo("view description"));
            Assert.That(result.Title, Is.EqualTo("Great system title"));
            Assert.That(result.Properties.Count, Is.EqualTo(2));
            Assert.That(result.Properties["key1"], Is.EqualTo("value1"));
            Assert.That(result.Properties["key2"], Is.EqualTo("value2"));
            Assert.That(result.Default, Is.False);
            Assert.That(result.FilterMode, Is.EqualTo("exclude"));
            Assert.That(result.Tags.Count, Is.EqualTo(2));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Tag1", "Tag2" }));
        }
    }
}
