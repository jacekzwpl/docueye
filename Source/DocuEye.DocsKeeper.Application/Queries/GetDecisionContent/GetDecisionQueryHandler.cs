using DocuEye.DocsKeeper.Persistence;
using Markdig;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.DocsKeeper.Application.Queries.GetDecisionContent
{
    /// <summary>
    /// Handler for GetDecisionQuery
    /// </summary>
    public class GetDecisionQueryHandler : IRequestHandler<GetDecisionQuery, DecisionContent?>
    {
        private readonly IDocsKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        public GetDecisionQueryHandler(IDocsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles query
        /// </summary>
        /// <param name="request">Query request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>Decision content or null if no was founded</returns>
        public async Task<DecisionContent?> Handle(GetDecisionQuery request, CancellationToken cancellationToken)
        {

            var decisionData = await this.dbContext.Decisions
                .FindOne(o => o.WorkspaceId == request.WorkspaceId 
                    &&  o.Id == request.Id);
            if(decisionData == null)
            {
                return null;
            }


            var pipeline = new MarkdownPipelineBuilder()
                .UseUrlRewriter(link =>
                    !string.IsNullOrEmpty(link.Url) && !link.Url.StartsWith("http") ?
                    request.BaseUrl.Replace("{documentationId}", decisionData.DocumentationId) + link.Url
                    : link.Url)
                .UseAdvancedExtensions().Build();

            var html = Markdown.ToHtml(decisionData.Content, pipeline);
            return new DecisionContent()
            {
                HtmlContent = html
            };
        }
    }
}
