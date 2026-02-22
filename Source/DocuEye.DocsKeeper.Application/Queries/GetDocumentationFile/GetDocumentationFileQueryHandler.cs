using DocuEye.DocsKeeper.Model;
using DocuEye.DocsKeeper.Persistence;
using DocuEye.Infrastructure.Mediator.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.DocsKeeper.Application.Queries.GetDocumentationFile
{
    public class GetDocumentationFileQueryHandler : IQueryHandler<GetDocumentationFileQuery, DocumentationFile?>
    {
        private readonly IDocsKeeperDBContext dbContext;

        public GetDocumentationFileQueryHandler(IDocsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<DocumentationFile?> HandleAsync(GetDocumentationFileQuery query, CancellationToken cancellationToken = default)
        {
            var documentationFileType = DocumentationFileType.MapFromDocumentationType(query.DocumentationType);
            return await this.dbContext.DocumentationFiles
                .FindOne(o => o.WorkspaceId == query.WorkspaceId
                && o.ElementId == query.ElementId
                && o.Type == documentationFileType);
        }
    }
}
