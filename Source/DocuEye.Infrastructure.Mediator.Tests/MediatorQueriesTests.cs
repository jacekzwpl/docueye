using Microsoft.Extensions.DependencyInjection;

namespace DocuEye.Infrastructure.Mediator.Tests
{
    public class MediatorQueriesTests : BaseTests
    {
        [Test]
        public async Task MediatorSendQueryShouldBeHandled()
        {
            // Arrange
            var mediator = serviceProvider.GetRequiredService<IMediator>();
            var query = new TestQuery("Test query");

            // Act
            var result = await mediator.SendQueryAsync<TestQuery, TestResult>(query);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Result, Is.EqualTo("Query executed: Test query"));

        }

        [Test]
        public void MediatorSendQueryWithCancelationTokenShouldBeUsed()
        {
            // Arrange
            var mediator = serviceProvider.GetRequiredService<IMediator>();
            var query = new TestQuery("Test query");

            // Act & Assert
            Assert.ThrowsAsync<OperationCanceledException>(async () =>
            {
                using var cts = new CancellationTokenSource();
                cts.Cancel(); // Cancel the token immediately
                await mediator.SendQueryAsync<TestQuery, TestResult>(query, cts.Token);
            });

        }
    }
}
