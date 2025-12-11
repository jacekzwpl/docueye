using DocuEye.ViewsKeeper.Application.Queries.GetDeploymentView;

namespace DocuEye.ViewsKeeper.Application.Tests.Queries
{
    public class GetDeploymentViewQueryHandlerTests : BaseViewsKeeperTests
    {
        [Test]
        public async Task WhenHanldeGetDeploymentViewQueryItemIsReturned()
        {
            // Arrange
            var query = new GetDeploymentViewQuery("DeploymentViewId1", "workspacetest1");

            // Act
            var handler = new GetDeploymentViewQueryHandler(this.dbContext);
            var item = await handler.HandleAsync(query, default);

            // Assert
            Assert.That(item, !Is.Null, "Hanlder should return item.");
        }

    }
}
