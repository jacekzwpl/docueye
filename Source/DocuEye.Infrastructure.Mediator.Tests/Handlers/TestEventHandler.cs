using DocuEye.Infrastructure.Mediator.Events;
using Microsoft.Extensions.DependencyInjection;

namespace DocuEye.Infrastructure.Mediator.Tests.Handlers
{
    public class TestEventHandler : IEventHandler<TestEvent>
    {
        private readonly IServiceProvider provider;
        public TestEventHandler(IServiceProvider provider)
        {
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }
        public Task HandleAsync(TestEvent @event, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var state = this.provider.GetRequiredService<EventHandlingState>();
            state.HandledEvents.Add($"Event executed: {@event.Name}");

            return Task.CompletedTask;
        }
    }
}
