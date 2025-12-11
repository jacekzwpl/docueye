using Microsoft.Extensions.DependencyInjection;

namespace DocuEye.Infrastructure.Mediator.Tests
{
    public class MediatorCommandsTests : BaseTests
    {
        [Test]
        public async Task MediatorSendCommandShouldBeHandled()
        {
            // Arrange
            var mediator = serviceProvider.GetRequiredService<IMediator>();
            var command = new TestCommand("Test command");

            // Act
            var result = await mediator.SendCommandAsync<TestCommand, TestResult>(command);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Result, Is.EqualTo("Command executed: Test command"));

        }

        [Test]
        public void MediatorSendCommandWithCancelationTokenShouldBeUsed()
        {
            // Arrange
            var mediator = serviceProvider.GetRequiredService<IMediator>();
            var command = new TestCommand("Test command");

            // Act & Assert
            Assert.ThrowsAsync<OperationCanceledException>(async () =>
            {
                using var cts = new CancellationTokenSource();
                cts.Cancel(); // Cancel the token immediately
                await mediator.SendCommandAsync<TestCommand, TestResult>(command, cts.Token);
            });

        }
    }
}
