using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.Infrastructure.Mediator.Queries
{
    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
    }
}
