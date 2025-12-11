using DocuEye.Infrastructure.Mediator.Events;
using DocuEye.Infrastructure.Mediator.Tests.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Infrastructure.Mediator.Tests
{
    public class MediatorEventsTests : BaseTests
    {
        [Test]
        public async Task MediatorSendEventShouldBeHandled()
        {
            // Arrange
            var mediator = serviceProvider.GetRequiredService<IMediator>();
            var eventData = new TestEvent("Test event");
            // Act
            await mediator.SendEventAsync<TestEvent>(eventData);
            // Assert
            var state = serviceProvider.GetRequiredService<EventHandlingState>();
            Assert.That(state.HandledEvents.First(), Is.EqualTo($"Event executed: {eventData.Name}"));
        }
        [Test]
        public void MediatorSendEventWithCancelationTokenShouldBeUsed()
        {
            // Arrange
            var mediator = serviceProvider.GetRequiredService<IMediator>();
            var eventData = new TestEvent("Test event");
            // Act & Assert
            Assert.ThrowsAsync<OperationCanceledException>(async () =>
            {
                using var cts = new CancellationTokenSource();
                cts.Cancel(); // Cancel the token immediately
                await mediator.SendEventAsync<TestEvent>(eventData, cts.Token);
            });
        }

        [Test]
        public async Task MediatorSendEventAllHandlersShouldBeUsed()
        {
            // Arrange
            var mediator = serviceProvider.GetRequiredService<IMediator>();
            

            var eventData = new TestEvent("Test event");
            // Act
            await mediator.SendEventAsync<TestEvent>(eventData);
            // Assert
            var state = serviceProvider.GetRequiredService<EventHandlingState>();
            Assert.That(state.HandledEvents.First(), Is.EqualTo($"Event executed: {eventData.Name}"));
            Assert.That(state.HandledEvents.Last(), Is.EqualTo($"Second handler executed: {eventData.Name}"));
        }
    }
}
