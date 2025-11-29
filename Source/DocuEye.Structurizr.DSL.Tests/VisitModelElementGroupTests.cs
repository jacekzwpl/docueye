using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitModelElementGroupTests : BaseTests
    {
        [Test]
        public void WhenGroupIsDefinedWithOutParrentThenAllPropertiesAreMatched()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            group Test {} 
            ");
            var context = parser.modelElementGroup();
            var visitor = new DSLVisitor();

            // Act
            //var result = visitor.VisitModelElementGroup(context, "system", StructurizrModelElementType.SoftwareSystem , null);

            // Assert
            //Assert.That(result.Name, Is.EqualTo("Test"));
            //Assert.That(result.ParentId, Is.Null);
        }

        [Test]
        public void WhenGroupIsDefinedWithParrentThenAllPropertiesAreMatched()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            group Test {} 
            ");
            var context = parser.modelElementGroup();
            var visitor = new DSLVisitor();

            // Act
            //var result = visitor.VisitModelElementGroup(context, "system", StructurizrModelElementType.SoftwareSystem, "parentId");

            // Assert
            //Assert.That(result.Name, Is.EqualTo("Test"));
            //Assert.That(result.ParentId, Is.EqualTo("parentId"));
        }

        [Test]
        public void WhenGroupIsDefinedWithElementsThenAllElementsExistsInModel()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            group Test {
                container Alice
                container System
            }");
            var context = parser.modelElementGroup();
            var visitor = new DSLVisitor();

            // Act
            //var result = visitor.VisitModelElementGroup(context, "system", StructurizrModelElementType.SoftwareSystem, null);

            // Assert
            //Assert.That(result.Name, Is.EqualTo("Test"));
            //Assert.That(visitor.Workspace.Model.Elements.Count, Is.EqualTo(2));
            //Assert.That(visitor.Workspace.Model.Elements.First().Name, Is.EqualTo("Alice"));
            //Assert.That(visitor.Workspace.Model.Elements.Last().Name, Is.EqualTo("System"));
        }
    }
}
