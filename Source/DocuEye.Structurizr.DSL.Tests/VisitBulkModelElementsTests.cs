using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitBulkModelElementsTests : BaseTests
    {
        [Test]
        public void WhenDefineBulkModelElements_ThenElementsAreUpdated()
        {
            // Arrange
            var workspace = new StructurizrWorkspace();
            workspace.Model.AddModelElement(new StructurizrPerson("elementId1", new string[] { "Element", "test" }));
            workspace.Model.AddModelElement(new StructurizrPerson("elementId2", new string[] { "Element", "test" }));
            workspace.Model.AddModelElement(new StructurizrPerson("elementId3", new string[] { "Element" }));
            var parser = CreateParserFromText(@"
            !elements ""element.tag==test"" {
                tags ""tag1,tag2""
            }
            ");
            var context = parser.bulkModelElements();
            var visitor = new DSLVisitor(workspace);
            // Act
            visitor.VisitBulkModelElements(context);
            // Assert
            Assert.That(
                visitor.Workspace.Model.Elements.SingleOrDefault(o => o.Identifier == "elementId1")?.Tags,
                Is.EquivalentTo(new[] { "Element", "test", "tag1", "tag2" }));
            Assert.That(
                visitor.Workspace.Model.Elements.SingleOrDefault(o => o.Identifier == "elementId2")?.Tags,
                Is.EquivalentTo(new[] { "Element", "test", "tag1", "tag2" }));
            Assert.That(
                visitor.Workspace.Model.Elements.SingleOrDefault(o => o.Identifier == "elementId3")?.Tags,
                Is.EquivalentTo(new[] { "Element" }));
        }
    }
}
