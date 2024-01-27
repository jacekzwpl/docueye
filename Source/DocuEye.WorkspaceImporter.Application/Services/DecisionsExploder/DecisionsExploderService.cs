using AutoMapper;
using DocuEye.Structurizr.Model;
using System.Collections.Generic;
using System.Linq;

namespace DocuEye.WorkspaceImporter.Application.Services.DecisionsExploder
{
    /// <summary>
    /// Service for exploding decisions from structurizr json
    /// </summary>
    public class DecisionsExploderService : IDecisionsExploderService
    {
        private readonly IMapper mapper;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="mapper">Automapper service</param>
        public DecisionsExploderService(IMapper mapper)
        {
            this.mapper = mapper;
        }
        /// <summary>
        /// Explodes decisions from structurizr json
        /// </summary>
        /// <param name="documentation">Documentation object form structurizr json</param>
        /// <returns>List of exploded decisions</returns>
        public List<ExplodedDecision> Explode(StructurizrDocumentation? documentation)
        {
            if(documentation == null || documentation.Decisions == null || !documentation.Decisions.Any())
            {
                return new List<ExplodedDecision>();
            }
            
            return this.mapper.Map<List<ExplodedDecision>>(documentation.Decisions);
        }
    }
}
