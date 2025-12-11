using DocuEye.Infrastructure.Mediator.Commands;
using DocuEye.Infrastructure.Mediator.Events;
using DocuEye.Infrastructure.Mediator.Queries;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.Infrastructure.Mediator
{
    public class Mediator : IMediator
    {
        private readonly IServiceProvider provider;

        public Mediator(IServiceProvider provider)
        {
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public async Task<TResult> SendCommandAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken = default) where TCommand : Commands.ICommand<TResult>
        {
            var handler = provider.GetRequiredService<ICommandHandler<TCommand, TResult>>();
            Func<Task<TResult>> handlerDelegate = () => handler.HandleAsync(command, cancellationToken);
            return await handlerDelegate();
        }

        public Task SendCommandAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand
        {
            throw new NotImplementedException();
        }

        public Task SendEventAsync<TEvent>(TEvent eventData, CancellationToken cancellationToken = default) where TEvent : IEvent
        {
            var handlers = provider.GetServices<IEventHandler<TEvent>>();
            Func<Task> handlerDelegate = () => Task.CompletedTask;
            foreach (var handler in handlers)
            {

                handlerDelegate = () => handler.HandleAsync(eventData, cancellationToken);
                // Execute each event handler asynchronously
                _ = handlerDelegate();
            }
            return Task.CompletedTask;
        }

        public async Task<TResult> SendQueryAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default) where TQuery : Queries.IQuery<TResult>
        {
            var handler = provider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
            Func<Task<TResult>> handlerDelegate = () => handler.HandleAsync(query, cancellationToken);
            return await handlerDelegate();
        }



    }
}
