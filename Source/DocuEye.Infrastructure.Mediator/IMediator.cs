using DocuEye.Infrastructure.Mediator.Commands;
using DocuEye.Infrastructure.Mediator.Events;
using DocuEye.Infrastructure.Mediator.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.Infrastructure.Mediator
{
    public interface IMediator
    {
        Task SendCommandAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand;
        Task<TResult> SendCommandAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken = default) where TCommand : Commands.ICommand<TResult>;
        Task<TResult> SendQueryAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default) where TQuery : Queries.IQuery<TResult>;
        Task SendEventAsync<TEvent>(TEvent eventData, CancellationToken cancellationToken = default) where TEvent : IEvent;
    }
}
