using DocuEye.DocsKeeper.Model;
using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Application.Services.DecisionsExploder;
using System.Collections.Generic;

namespace DocuEye.WorkspaceImporter.Application.Services.ModelExploder
{
    /// <summary>
    /// Element exploded from structurizr json
    /// </summary>
    public class ExplodedElement : Element
    {
        /// <summary>
        /// The ID of the element in structurizr json
        /// </summary>
        public string StructurizrId { get; set; } = null!;
        /// <summary>
        /// The ID of parent element in structurizr json
        /// </summary>
        public string? StructurizrParentId { get; set; }
        /// <summary>
        /// The ID of element in structurizr json that this element is instance of
        /// </summary>
        public string? StructurizrInstanceOfId { get; set; }
        /// <summary>
        /// Documentation sections that belong to this element
        /// </summary>
        public IEnumerable<DocumentationSection>? DocumentationSections { get; set; }
        /// <summary>
        /// Documentation images that belong to this element
        /// </summary>
        public IEnumerable<Image>? DocumentationImages { get; set; }
        /// <summary>
        /// Exploded decisions that belong to this element
        /// </summary>
        public IEnumerable<ExplodedDecision>? DocumentationDecisions { get; set; }

    }
}
