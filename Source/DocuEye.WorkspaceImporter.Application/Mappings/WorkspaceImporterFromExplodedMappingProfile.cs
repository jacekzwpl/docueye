using AutoMapper;
using DocuEye.DocsKeeper.Model;
using DocuEye.ModelKeeper.Model;
using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspaceImporter.Application.Services.DecisionsExploder;
using DocuEye.WorkspaceImporter.Application.Services.ModelExploder;
using DocuEye.WorkspaceImporter.Application.Services.ViewsExploder;

namespace DocuEye.WorkspaceImporter.Application.Mappings
{
    /// <summary>
    /// Automapper profile for mappings from exploded items 
    /// </summary>
    public class WorkspaceImporterFromExplodedMappingProfile : Profile
    {
        /// <summary>
        /// Creates instance
        /// </summary>
        public WorkspaceImporterFromExplodedMappingProfile() {

            
            CreateMap<ExplodedDecision, Decision>();
            CreateMap<ExplodedDecisionLink, DecisionLink>();

            CreateMap<ExplodedElement, Element>();
            CreateMap<ExplodedRelationship, Relationship>();

            CreateMap<ExplodedElementView, ElementView>();
            CreateMap<ExplodedElement, ElementView>();
            CreateMap<ExplodedRelationshipView, RelationshipView>();
            CreateMap<ExplodedRelationship, RelationshipView>();

            CreateMap<ExplodedRelationshipView, DynamicRelationshipView>();

        }
    }
}
