namespace DocuEye.Structurizr.DSL.Tests
{
    public class WorkspaceTests : BaseTests
    {

        

        [Test]
        public void WhenSetBasicPropsInLineThenPropertiesHasValidValues()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            workspace ""Test Workspace"" ""Test Workspace Description"" {}
            ");
            StructurizrDSLParser.WorkspaceContext worskspaceContext = parser.workspace();
            var visitor = new DSLVisitor();

            // Act
            visitor.VisitWorkspace(worskspaceContext);

            // Assert
            Assert.That(visitor.Workspace.Name, Is.EqualTo("Test Workspace"));
            Assert.That(visitor.Workspace.Description, Is.EqualTo("Test Workspace Description"));
        }

        [Test]
        public void WhenSetOnlyNameThenOnlyNameIsSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            workspace ""Test Workspace"" {}
            ");
            StructurizrDSLParser.WorkspaceContext worskspaceContext = parser.workspace();
            var visitor = new DSLVisitor();

            // Act
            visitor.VisitWorkspace(worskspaceContext);

            // Assert
            Assert.That(visitor.Workspace.Name, Is.EqualTo("Test Workspace"));
            Assert.That(visitor.Workspace.Description, Is.Null);
        }
    }
}