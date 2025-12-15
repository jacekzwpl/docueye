using DocuEye.Structurizr.DSL.Model;
using DocuEye.Structurizr.DSL.Tests.HttpHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitIncludeFileTests : BaseTests
    {
        [Test]
        public void WhenIncludeWorkspaceBodyFileThenWorkspaceIsModified()
        {

            // Arrange
            var parser = CreateParserFromText(@"!include ./TestData/IncludeFile/workspacebody.dsl
            ");
            var context = parser.includeFile();
            var visitor = new DSLVisitor();
            // Act
            visitor.VisitIncludeFileWorkspaceBody(context);

            // Assert
            Assert.That(visitor.Workspace.Model.Elements.Count(), Is.EqualTo(2));
            Assert.That(visitor.Workspace.Model.Relationships.Count(), Is.EqualTo(1));
            Assert.That(parser.NumberOfSyntaxErrors, Is.EqualTo(0));

        }

        [Test]
        public void WhenIncludeWorkspaceBodyFileFromUrlThenWorkspaceIsModified()
        {

            // Arrange
            var parser = CreateParserFromText(@"!include https://docueye.com/test.dsl
            ");
            var context = parser.includeFile();
            var visitor = new DSLVisitor(
                new StructurizrWorkspace(), 
                new HttpClient(new IncludeFileHttpMessageHandler("./TestData/IncludeFile/workspacebody.dsl")));
            // Act
            visitor.VisitIncludeFileWorkspaceBody(context);

            // Assert
            Assert.That(visitor.Workspace.Model.Elements.Count(), Is.EqualTo(2));
            Assert.That(visitor.Workspace.Model.Relationships.Count(), Is.EqualTo(1));
            Assert.That(parser.NumberOfSyntaxErrors, Is.EqualTo(0));

        }

        [Test]
        public void WhenIncludeModelBodyFileThenWorkspaceIsModified()
        {

            // Arrange
            var parser = CreateParserFromText(@"!include ./TestData/IncludeFile/modelbody.dsl
            ");
            var context = parser.includeFile();
            var visitor = new DSLVisitor();
            // Act
            visitor.VisitIncludeFileModelBody(context);

            // Assert
            Assert.That(visitor.Workspace.Model.Elements.Count(), Is.EqualTo(2));
            Assert.That(visitor.Workspace.Model.Relationships.Count(), Is.EqualTo(1));
            Assert.That(parser.NumberOfSyntaxErrors, Is.EqualTo(0));

        }

        [Test]
        public void WhenIncludeModelGroupBodyFileThenWorkspaceIsModified()
        {

            // Arrange
            var parser = CreateParserFromText(@"!include ./TestData/IncludeFile/modelbody.dsl
            ");
            var context = parser.includeFile();
            var visitor = new DSLVisitor();
            // Act
            visitor.VisitIncludeFileModelGroupBody(context, "1");

            // Assert
            Assert.That(visitor.Workspace.Model.Elements.Count(), Is.EqualTo(2));
            Assert.That(visitor.Workspace.Model.Elements.First().GroupId, Is.EqualTo("1"));
            Assert.That(visitor.Workspace.Model.Elements.Last().GroupId, Is.EqualTo("1"));
            Assert.That(visitor.Workspace.Model.Relationships.Count(), Is.EqualTo(1));
            Assert.That(parser.NumberOfSyntaxErrors, Is.EqualTo(0));


        }
    }
}
