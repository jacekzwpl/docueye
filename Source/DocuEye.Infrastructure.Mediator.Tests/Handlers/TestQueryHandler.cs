using DocuEye.Infrastructure.Mediator.Queries;

namespace DocuEye.Infrastructure.Mediator.Tests.Handlers
{
    public class TestQueryHandler : IQueryHandler<TestQuery, TestResult>
    {
        public Task<TestResult> HandleAsync(TestQuery query, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(new TestResult($"Query executed: {query.Name}"));
        }
    }
}
