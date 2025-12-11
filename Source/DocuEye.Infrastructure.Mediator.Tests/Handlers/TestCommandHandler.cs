using DocuEye.Infrastructure.Mediator.Commands;

namespace DocuEye.Infrastructure.Mediator.Tests.Handlers
{
    public class TestCommandHandler : ICommandHandler<TestCommand, TestResult>
    {
        public Task<TestResult> HandleAsync(TestCommand command, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(new TestResult($"Command executed: {command.Name}"));
        }
    }
}
