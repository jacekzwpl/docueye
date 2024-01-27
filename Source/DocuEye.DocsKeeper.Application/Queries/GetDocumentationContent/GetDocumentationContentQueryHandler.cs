using DocuEye.DocsKeeper.Persistence;
using Markdig;
using MediatR;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.DocsKeeper.Application.Queries.GetWorkspaceDocumentation
{
    /// <summary>
    /// Handler for GetDocumentationContentQuery
    /// </summary>
    public class GetDocumentationContentQueryHandler : IRequestHandler<GetDocumentationContentQuery, DocumentationContent?>
    {
        private readonly IDocsKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        public GetDocumentationContentQueryHandler(IDocsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles query
        /// </summary>
        /// <param name="request">Query request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>Documentation content or null if no was found</returns>
        public async Task<DocumentationContent?> Handle(GetDocumentationContentQuery request, CancellationToken cancellationToken)
        {
            var documentationData =  await this.dbContext.Documentations
                .FindOne(o => o.WorkspaceId == request.WorkspaceId && o.ElementId == request.ElementId);
            if (documentationData == null)
            {
                return null;
            }

            var stringBuilder = new StringBuilder();
            

            foreach(var section in documentationData.Sections)
            {
                stringBuilder.AppendLine(section.Content);
            }

            var imageUrl = request.BaseUrl + 
                string.Format("/api/workspaces/{0}/documentations/{1}/images/", 
                    request.WorkspaceId, documentationData.Id);

            var pipeline = new MarkdownPipelineBuilder()
                .UseUrlRewriter(link => 
                    !string.IsNullOrEmpty(link.Url) && !link.Url.StartsWith("http") ?
                    imageUrl + link.Url 
                    : link.Url)
                .Build();

            var html = Markdown.ToHtml(stringBuilder.ToString(), pipeline);
            return new DocumentationContent()
            {
                HtmlContent = html
            };
            
        }
    }
}
