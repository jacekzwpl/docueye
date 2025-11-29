using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StructurizrDSLParser;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitModelGroupsTests : BaseTests
    {
        [Test]
        public void WhenMultipleGroupsAreDefinedWithOutParrentThenAllPropertiesAreMatched()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            group Test1 {}
            ");
            var visitor = new DSLVisitor();
            var modelContext = parser.modelGroup();
            // Act
            visitor.VisitModelGroups(new ModelGroupContext[] {
                modelContext , modelContext
            }, null);
            // Assert
            Assert.That(visitor.Workspace.Model.Groups.Count, Is.EqualTo(2));
            Assert.That(visitor.Workspace.Model.Groups.First().Name, Is.EqualTo("Test1"));
            Assert.That(visitor.Workspace.Model.Groups.Last().Name, Is.EqualTo("Test1"));
            Assert.That(visitor.Workspace.Model.Groups.First().ParentId, Is.Null);
            Assert.That(visitor.Workspace.Model.Groups.Last().ParentId, Is.Null);

        }

        [Test]
        public void WhenMultipleGroupsAreDefinedWithParrentThenAllPropertiesAreMatched()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            group Test1 {}
            ");
            var visitor = new DSLVisitor();
            var modelContext = parser.modelGroup();
            // Act
            visitor.VisitModelGroups(new ModelGroupContext[] {
                modelContext , modelContext
            }, "parentID");
            // Assert
            Assert.That(visitor.Workspace.Model.Groups.Count, Is.EqualTo(2));
            Assert.That(visitor.Workspace.Model.Groups.First().Name, Is.EqualTo("Test1"));
            Assert.That(visitor.Workspace.Model.Groups.Last().Name, Is.EqualTo("Test1"));
            Assert.That(visitor.Workspace.Model.Groups.First().ParentId, Is.EqualTo("parentID"));
            Assert.That(visitor.Workspace.Model.Groups.Last().ParentId, Is.EqualTo("parentID"));

        }
    }
}
