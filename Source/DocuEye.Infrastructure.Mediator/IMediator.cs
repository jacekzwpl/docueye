using DocuEye.Infrastructure.Mediator.Commands;
using DocuEye.Infrastructure.Mediator.Events;
using DocuEye.Infrastructure.Mediator.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.Infrastructure.Mediator
{
    public interface IMediator
    {
        Task<TResult> SendCommandAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand<TResult>;

        Task<TResult> SendQueryAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default) where TQuery : IQuery<TResult>;

        Task SendEventAsync<TEvent>(TEvent eventData, CancellationToken cancellationToken = default) where TEvent : IEvent;
    }
}
