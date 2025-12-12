using DocuEye.Infrastructure.Mediator.Commands;
using DocuEye.Infrastructure.Mediator.Tests.Model;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework.Internal.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Infrastructure.Mediator.Tests.Handlers
{
    public class TestVoidCommandHandler : ICommandHandler<TestVoidCommand>
    {
        private readonly IServiceProvider provider;
        public TestVoidCommandHandler(IServiceProvider provider)
        {
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }
        public Task HandleAsync(TestVoidCommand command, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var state = this.provider.GetRequiredService<EventHandlingState>();
            state.HandledEvents.Add($"Void command executed: {command.Name}");
            return Task.CompletedTask;
        }
    }
}
