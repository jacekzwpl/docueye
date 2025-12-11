using DocuEye.Infrastructure.Mediator.Events;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Infrastructure.Mediator.Tests.Handlers
{
    public class SecondEventHandler : IEventHandler<TestEvent>
    {
        private readonly IServiceProvider provider;
        public SecondEventHandler(IServiceProvider provider)
        {
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }
        public Task HandleAsync(TestEvent @event, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var state = this.provider.GetRequiredService<EventHandlingState>();
            state.HandledEvents.Add($"Second handler executed: {@event.Name}");

            return Task.CompletedTask;
        }
    }
}
