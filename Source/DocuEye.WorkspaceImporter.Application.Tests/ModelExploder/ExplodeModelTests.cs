using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Application.Services.ModelExploder;

namespace DocuEye.WorkspaceImporter.Application.Tests.ModelExploder
{
    public class ExplodeModelTests : BaseModelExploderTests
    {
        
        [Test]
        public void WhenExplodeModelThanExpectedAllElementsArePresentTest()
        {
            var exploder = new ModelExploderService(mapper);
            var result = exploder.ExplodeModel(model);
            Assert.That(result.Elements.Count, Is.EqualTo(9), "Exploded model should contain 9 elements.");
        }

        [Test]
        public void WhenExplodeModelThanExpectedSoftwareSystemsArePresentTest()
        {
            var exploder = new ModelExploderService(mapper);
            var result = exploder.ExplodeModel(model);
            Assert.That(result.Elements.Where(o => o.Type == ElementType.SoftwareSystem).Count(), Is.EqualTo(1), "Exploded model should contain 1 Software System.");
        }

        [Test]
        public void WhenExplodeModelThanExpectedContainersArePresentTest()
        {
            var exploder = new ModelExploderService(mapper);
            var result = exploder.ExplodeModel(model);
            Assert.That(result.Elements.Where(o => o.Type == ElementType.Container).Count(), Is.EqualTo(1), "Exploded model should contain 1 Container.");
        }

        [Test]
        public void WhenExplodeModelThanExpectedComponentsArePresentTest()
        {
            var exploder = new ModelExploderService(mapper);
            var result = exploder.ExplodeModel(model);
            Assert.That(result.Elements.Where(o => o.Type == ElementType.Component).Count(), Is.EqualTo(1), "Exploded model should contain 1 Component.");
        }

        [Test]
        public void WhenExplodeModelThanExpectedPeopleArePresentTest()
        {
            var exploder = new ModelExploderService(mapper);
            var result = exploder.ExplodeModel(model);
            Assert.That(result.Elements.Where(o => o.Type == ElementType.Person).Count(), Is.EqualTo(1), "Exploded model should contain 1 Person.");
        }

        [Test]
        public void WhenExplodeModelThanExpectedDeploymentNodesArePresentTest()
        {
            var exploder = new ModelExploderService(mapper);
            var result = exploder.ExplodeModel(model);
            Assert.That(result.Elements.Where(o => o.Type == ElementType.DeploymentNode).Count(), Is.EqualTo(3), "Exploded model should contain 3 Deployment Nodes.");
        }

        [Test]
        public void WhenExplodeModelThanExpectedContainerInstancesArePresentTest()
        {
            var exploder = new ModelExploderService(mapper);
            var result = exploder.ExplodeModel(model);
            Assert.That(result.Elements.Where(o => o.Type == ElementType.ContainerInstance).Count(), Is.EqualTo(1), "Exploded model should contain 1 Container Instance.");
        }

        [Test]
        public void WhenExplodeModelThanExpectedSoftwareSystemInstancesArePresentTest()
        {
            var exploder = new ModelExploderService(mapper);
            var result = exploder.ExplodeModel(model);
            Assert.That(result.Elements.Where(o => o.Type == ElementType.SoftwareSystemInstance).Count(), Is.EqualTo(1), "Exploded model should contain 1 Software System Instance.");
        }


        [Test]
        public void WhenExplodeModelThanExpectedAllRelationshipsArePresentTest()
        {
            var exploder = new ModelExploderService(mapper);
            var result = exploder.ExplodeModel(model);
            Assert.That(result.Relationships.Count, Is.EqualTo(5), "Exploded model should contain 5 relationships.");
        }
    }
}
