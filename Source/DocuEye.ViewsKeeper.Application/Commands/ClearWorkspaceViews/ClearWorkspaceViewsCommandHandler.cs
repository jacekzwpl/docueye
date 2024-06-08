using DocuEye.ViewsKeeper.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ViewsKeeper.Application.Commands.ClearWorkspaceViews
{
    public class ClearWorkspaceViewsCommandHandler : IRequestHandler<ClearWorkspaceViewsCommand>
    {
        private readonly IViewsKeeperDBContext dBContext;
        public ClearWorkspaceViewsCommandHandler(IViewsKeeperDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task Handle(ClearWorkspaceViewsCommand request, CancellationToken cancellationToken)
        {
            await this.dBContext.AllViews.DeleteManyAsync(o => o.WorkspaceId == request.WorkspaceId);
        }
    }
}
