using System.Collections.Generic;

namespace DocuEye.WorkspaceImporter.Application.Services.ModelExploder
{
    /// <summary>
    /// The result of exploding model elements and relationships from structurizr json
    /// </summary>
    public class ExplodeResult
    {
        /// <summary>
        /// List of exploded elements
        /// </summary>
        public List<ExplodedElement> Elements = new List<ExplodedElement>();
        /// <summary>
        /// List of exploded relationships
        /// </summary>
        public List<ExplodedRelationship> Relationships = new List<ExplodedRelationship>();
    }
}
