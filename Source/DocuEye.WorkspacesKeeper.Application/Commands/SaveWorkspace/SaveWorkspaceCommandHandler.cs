using DocuEye.Infrastructure.Mediator.Commands;
using DocuEye.WorkspacesKeeper.Persistence;

using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspacesKeeper.Application.Commands.SaveWorkspace
{
    /// <summary>
    /// Handler for SaveWorkspaceCommand
    /// </summary>
    public class SaveWorkspaceCommandHandler : ICommandHandler<SaveWorkspaceCommand>
    {
        private readonly IWorkspacesKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        public SaveWorkspaceCommandHandler(IWorkspacesKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles command
        /// </summary>
        /// <param name="request">Command request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        public async Task HandleAsync(SaveWorkspaceCommand request, CancellationToken cancellationToken)
        {
            await this.dbContext.Workspaces
                .UpsertOneAsync(request.Workspace, cancellationToken);
        }
    }
}
