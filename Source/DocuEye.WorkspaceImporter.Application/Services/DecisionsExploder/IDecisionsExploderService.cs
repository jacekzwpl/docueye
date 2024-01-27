using DocuEye.Structurizr.Model;
using System.Collections.Generic;

namespace DocuEye.WorkspaceImporter.Application.Services.DecisionsExploder
{
    /// <summary>
    /// Service interface for exploding decisions from structurizr json
    /// </summary>
    public interface IDecisionsExploderService
    {
        /// <summary>
        /// Explodes decisions from structurizr json
        /// </summary>
        /// <param name="documentation">Documentation object form structurizr json</param>
        /// <returns>List of exploded decisions</returns>
        List<ExplodedDecision> Explode(StructurizrDocumentation? documentation);
    }
}
