using DocuEye.Infrastructure.Mediator.Commands;
using DocuEye.ViewsKeeper.Model;
using DocuEye.ViewsKeeper.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ViewsKeeper.Application.Commands.SaveViewLayout
{
    internal class SaveViewLayoutCommandHandler : ICommandHandler<SaveViewLayoutCommand>
    {
        private readonly IViewsKeeperDBContext dbContext;
        public SaveViewLayoutCommandHandler(IViewsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task HandleAsync(SaveViewLayoutCommand command, CancellationToken cancellationToken = default)
        {
            var viewLayout = new ViewLayout
            {
                WorkspaceId = command.WorkspaceId,
                Id = command.ViewId,
                LayoutData = command.LayoutData
            };
            await this.dbContext.ViewLayouts.UpsertOneAsync(viewLayout);
        }
    }
}
