using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitModelGroupTests : BaseTests
    {
        [Test]
        public void WhenGroupIsDefinedWithOutParrentThenAllPropertiesAreMatched()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            group Test {} 
            ");
            var modelContext = parser.modelGroup();
            var visitor = new DSLVisitor();

            // Act
            var result = visitor.VisitModelGroup(modelContext,null);

            // Assert
            Assert.That(result.Name, Is.EqualTo("Test"));
            Assert.That(result.ParentId, Is.Null);
        }

        [Test]
        public void WhenGroupIsDefinedWithParrentThenAllPropertiesAreMatched()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            group Test {} 
            ");
            var modelContext = parser.modelGroup();
            var visitor = new DSLVisitor();

            // Act
            var result = visitor.VisitModelGroup(modelContext, "parentId");

            // Assert
            Assert.That(result.Name, Is.EqualTo("Test"));
            Assert.That(result.ParentId, Is.EqualTo("parentId"));
        }

        [Test]
        public void WhenGroupIsDefinedWithElementsThenAllElementsExistsInModel()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            group Test {
                person Alice
                softwareSystem System
            } 
            ");
            var modelContext = parser.modelGroup();
            var visitor = new DSLVisitor();

            // Act
            var result = visitor.VisitModelGroup(modelContext, null);

            // Assert
            Assert.That(result.Name, Is.EqualTo("Test"));
            Assert.That(visitor.Workspace.Model.Elements.Count, Is.EqualTo(2));
            Assert.That(visitor.Workspace.Model.Elements.First().Name, Is.EqualTo("Alice"));
            Assert.That(visitor.Workspace.Model.Elements.Last().Name, Is.EqualTo("System"));
        }
    }
}
