using DocuEye.WorkspacesKeeper.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspacesKeeper.Application.Commands.SaveViewConfiguration
{
    /// <summary>
    /// Handler for SaveViewConfigurationCommand
    /// </summary>
    public class SaveViewConfigurationCommandHandler : IRequestHandler<SaveViewConfigurationCommand>
    {
        private readonly IWorkspacesKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        public SaveViewConfigurationCommandHandler(IWorkspacesKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles command
        /// </summary>
        /// <param name="request">Command request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        public async Task Handle(SaveViewConfigurationCommand request, CancellationToken cancellationToken)
        {
            await this.dbContext.ViewConfigurations
                .UpsertOneAsync(request.ViewConfiguration, cancellationToken);
        }
    }
}
