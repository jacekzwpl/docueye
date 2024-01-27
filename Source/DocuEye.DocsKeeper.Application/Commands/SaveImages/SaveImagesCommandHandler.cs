using DocuEye.DocsKeeper.Application.Commads.SaveDocumentationChanges;
using DocuEye.DocsKeeper.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DocuEye.DocsKeeper.Application.Commads.SaveImages;
using System.Linq;

namespace DocuEye.DocsKeeper.Application.Commands.SaveImages
{
    /// <summary>
    /// Handler for SaveImagesCommand
    /// </summary>
    public class SaveImagesCommandHandler : IRequestHandler<SaveImagesCommand>
    {
        private readonly IDocsKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongDB context</param>
        public SaveImagesCommandHandler(IDocsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles command
        /// </summary>
        /// <param name="request">command request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        public async Task Handle(SaveImagesCommand request, CancellationToken cancellationToken)
        {
            //Delete all images
            await this.dbContext.Images.DeleteManyAsync(o => o.WorkspaceId == request.WorkspaceId);
            // Create new images
            if (request.ImagesToAdd.Count() > 0)
            {
                await this.dbContext.Images.InsertManyAsync(request.ImagesToAdd);
            }

        }
    }
}
