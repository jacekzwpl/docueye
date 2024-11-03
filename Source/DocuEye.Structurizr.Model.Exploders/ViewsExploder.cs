using AutoMapper;
using DocuEye.WorkspaceImporter.Api.Model.ViewConfiguration;
using DocuEye.WorkspaceImporter.Api.Model.Views;
using System.Collections.Generic;

namespace DocuEye.Structurizr.Model.Exploders
{
    public class ViewsExploder
    {
        private readonly IMapper mapper;

        public ViewsExploder(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public IEnumerable<ViewToImport> ExplodeSystemLandscapeViews(IEnumerable<StructurizrSystemLandscapeView> views) { 
            return this.mapper.Map<IEnumerable<ViewToImport>>(views);
        }

        public IEnumerable<ViewToImport> ExplodeSystemContextViews(IEnumerable<StructurizrSystemContextView> views) { 
            return this.mapper.Map<IEnumerable<ViewToImport>>(views);
        }

        public IEnumerable<ViewToImport> ExplodeContainerViews(IEnumerable<StructurizrContainerView> views) { 
            return this.mapper.Map<IEnumerable<ViewToImport>>(views);
        }

        public IEnumerable<ViewToImport> ExplodeComponentViews(IEnumerable<StructurizrComponentView> views) { 
            return this.mapper.Map<IEnumerable<ViewToImport>>(views);
        }

        public IEnumerable<ViewToImport> ExplodeDynamicViews(IEnumerable<StructurizrDynamicView> views) { 
            return this.mapper.Map<IEnumerable<ViewToImport>>(views);
        }

        public IEnumerable<ViewToImport> ExplodeDeploymentViews(IEnumerable<StructurizrDeploymentView> views) { 
            return this.mapper.Map<IEnumerable<ViewToImport>>(views);
        }

        public IEnumerable<ViewToImport> ExplodeFilteredViews(IEnumerable<StructurizrFilteredView> views) { 
            return this.mapper.Map<IEnumerable<ViewToImport>>(views);
        }

        public IEnumerable<ViewToImport> ExplodeImageViews(IEnumerable<StructurizrImageView> views) { 
            return this.mapper.Map<IEnumerable<ViewToImport>>(views);
        }

        public ViewConfigurationToImport ExplodeViewConfiguration(StructurizrConfiguration viewConfiguration)
        {
            return new ViewConfigurationToImport()
            {
                ElementStyles = this.mapper.Map<IEnumerable<ElementStyleToImport>>(viewConfiguration.Styles?.Elements),
                RelationshipStyles = this.mapper.Map<IEnumerable<RelationshipStyleToImport>>(viewConfiguration.Styles?.Relationships),
                Terminology = this.mapper.Map<TerminologyToImport>(viewConfiguration.Terminology),
                Themes = viewConfiguration.Themes,
            };
        }
    }
}
