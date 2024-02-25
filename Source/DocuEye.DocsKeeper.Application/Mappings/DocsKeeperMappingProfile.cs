using AutoMapper;
using DocuEye.DocsKeeper.Application.Queries.FindDecisions;
using DocuEye.DocsKeeper.Application.Queries.GetDecisionsList;
using DocuEye.DocsKeeper.Model;

namespace DocuEye.DocsKeeper.Application.Mappings
{
    /// <summary>
    /// Automapper profile for DocsKeeper module
    /// </summary>
    public class DocsKeeperMappingProfile : Profile
    {
        /// <summary>
        /// Creates instance
        /// </summary>
        public DocsKeeperMappingProfile() {
            CreateMap<Decision, FoundedDecision>();
            CreateMap<Decision, DecisionsListItem>();
        }
    }
}
