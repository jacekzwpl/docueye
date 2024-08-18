namespace DocuEye.DocsKeeper.Application.Queries.GetDecisionsList
{
    public class DecisionsListItem
    {
        /// <summary>
        /// Decision Id
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// Decision DSL ID
        /// </summary>
        public string DslId { get; set; } = null!;
        /// <summary>
        /// Workspace ID
        /// </summary>
        public string WorkspaceId { get; set; } = null!;
        /// <summary>
        /// Documentation ID
        /// </summary>
        public string DocumentationId { get; set; } = null!;
        /// <summary>
        /// Element ID
        /// </summary>
        public string? ElementId { get; set; }
    }
}
