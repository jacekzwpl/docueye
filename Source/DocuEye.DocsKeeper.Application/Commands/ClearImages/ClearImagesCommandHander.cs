using DocuEye.DocsKeeper.Application.Commads.ClearImages;
using DocuEye.DocsKeeper.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.DocsKeeper.Application.Commands.ClearImages
{
    public class ClearImagesCommandHander : IRequestHandler<ClearImagesCommand>
    {
        private readonly IDocsKeeperDBContext dbContext;

        public ClearImagesCommandHander(IDocsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task Handle(ClearImagesCommand request, CancellationToken cancellationToken)
        {
            await this.dbContext.Images.DeleteManyAsync(o => o.WorkspaceId == request.WorkspaceId);
        }
    }
}
