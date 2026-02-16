using DocuEye.Infrastructure.Mediator.Commands;
using DocuEye.IssueTracker.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.IssueTracker.Application.Commands.SaveIssues
{
    public class SaveIssuesCommandHandler : ICommandHandler<SaveIssuesCommand>
    {
        private readonly IIssueTrackerDBContext dbContext;

        public SaveIssuesCommandHandler(IIssueTrackerDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task HandleAsync(SaveIssuesCommand request, CancellationToken cancellationToken)
        {
            await this.dbContext.Issues.DeleteManyAsync(o => o.WorkspaceId == request.WorkspaceId);
            if(request.Issues != null && request.Issues.Count() > 0)
            {
                await this.dbContext.Issues.InsertManyAsync(request.Issues);
            }
        }
    }
}
