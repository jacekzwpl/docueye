using DocuEye.Structurizr.DSL.Model;

namespace DocuEye.Structurizr.DSL.Tests.FileTests
{
    public class BigBankTest : BaseTests
    {
        [Test]
        public void WhenBigBankWorkspaceIsParsedThenCorrectValuesAreReturned()
        {
            // Arrange
            var content = File.ReadAllText("./TestData/Workspace/big-bank.dsl");
            var parser = CreateParserFromText(content);
            var context = parser.workspace();
            var visitor = new DSLVisitor();
            // Act
            visitor.VisitWorkspace(context);
            // Assert
            Assert.That(visitor.Workspace.Name, Is.EqualTo("Big Bank plc"));
            Assert.That(visitor.Workspace.Model.Elements.Count(), Is.EqualTo(51));
            Assert.That(
                visitor.Workspace.Model.Elements
                .Where(o => o.Type == StructurizrModelElementType.Person)
                .Count(), Is.EqualTo(3));
            Assert.That(
                visitor.Workspace.Model.Elements
                .Where(o => o.Type == StructurizrModelElementType.SoftwareSystem)
                .Count(), Is.EqualTo(4));
            Assert.That(
                visitor.Workspace.Model.Elements
                .Where(o => o.Type == StructurizrModelElementType.Container)
                .Count(), Is.EqualTo(5));
            Assert.That(
                visitor.Workspace.Model.Elements
                .Where(o => o.Type == StructurizrModelElementType.Component)
                .Count(), Is.EqualTo(6));
            Assert.That(
                visitor.Workspace.Model.Elements
                .Where(o => o.Type == StructurizrModelElementType.DeploymentNode)
                .Count(), Is.EqualTo(21));
            Assert.That(
                visitor.Workspace.Model.Elements
                .Where(o => o.Type == StructurizrModelElementType.ContainerInstance)
                .Count(), Is.EqualTo(10));
            Assert.That(
                visitor.Workspace.Model.Elements
                .Where(o => o.Type == StructurizrModelElementType.SoftwareSystemInstance)
                .Count(), Is.EqualTo(2));

            Assert.That(visitor.Workspace.Model.Relationships
                .Where(o => o.LinkedRelationshipIdentifier == null).Count(), Is.EqualTo(27));
            Assert.That(visitor.Workspace.Model.Groups.Count(), Is.EqualTo(1));

            Assert.That(visitor.Workspace.Model.DeploymentEnvironments.Count(), Is.EqualTo(2));

            Assert.That(visitor.Workspace.Views.SystemLandscapeViews.Count(), 
                Is.EqualTo(1));
            Assert.That(visitor.Workspace.Views.SystemContextViews.Count(),
                Is.EqualTo(1));
            Assert.That(visitor.Workspace.Views.ContainerViews.Count(),
                Is.EqualTo(1));
            Assert.That(visitor.Workspace.Views.ComponentViews.Count(),
                Is.EqualTo(1));
            Assert.That(visitor.Workspace.Views.ImageViews.Count(), 
                Is.EqualTo(1));
            Assert.That(visitor.Workspace.Views.DynamicViews.Count(),
                Is.EqualTo(1));
            Assert.That(visitor.Workspace.Views.DeploymentViews.Count(),
                Is.EqualTo(2));

            Assert.That(visitor.Workspace.Views.Styles?.ElementStyles.Count(),
                Is.EqualTo(11));


        }
    }
}
