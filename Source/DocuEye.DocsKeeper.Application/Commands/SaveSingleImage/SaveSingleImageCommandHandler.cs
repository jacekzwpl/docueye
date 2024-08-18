using DocuEye.DocsKeeper.Application.Commads.SaveSingleImage;
using DocuEye.DocsKeeper.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.DocsKeeper.Application.Commands.SaveSingleImage
{
    public class SaveSingleImageCommandHandler : IRequestHandler<SaveSingleImageCommand>
    {
        private readonly IDocsKeeperDBContext dbContext;
        public SaveSingleImageCommandHandler(IDocsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task Handle(SaveSingleImageCommand request, CancellationToken cancellationToken)
        {
            await this.dbContext.Images.InsertOneAsync(request.Image);
        }
    }
}
