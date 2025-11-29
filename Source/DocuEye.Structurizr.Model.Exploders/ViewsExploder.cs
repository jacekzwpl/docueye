using AutoMapper;
using DocuEye.Structurizr.Json.Model;
using DocuEye.WorkspaceImporter.Api.Model.ViewConfiguration;
using DocuEye.WorkspaceImporter.Api.Model.Views;
using System.Collections.Generic;
using System.Linq;

namespace DocuEye.Structurizr.Model.Exploders
{
    public class ViewsExploder
    {
        private readonly IMapper mapper;

        public ViewsExploder(IMapper mapper)
        {
            this.mapper = mapper;
        }


        public IEnumerable<ViewToImport> ExplodeSystemLandscapeViews(IEnumerable<StructurizrJsonSystemLandscapeView>? views) { 
            if(views == null)
            {
                return Enumerable.Empty<ViewToImport>();
            }
            return this.mapper.Map<IEnumerable<ViewToImport>>(views);
        }

        public IEnumerable<ViewToImport> ExplodeSystemContextViews(IEnumerable<StructurizrJsonSystemContextView>? views) {
            if (views == null)
            {
                return Enumerable.Empty<ViewToImport>();
            }
            return this.mapper.Map<IEnumerable<ViewToImport>>(views);
        }

        public IEnumerable<ViewToImport> ExplodeContainerViews(IEnumerable<StructurizrJsonContainerView>? views) {
            if (views == null)
            {
                return Enumerable.Empty<ViewToImport>();
            }
            return this.mapper.Map<IEnumerable<ViewToImport>>(views);
        }

        public IEnumerable<ViewToImport> ExplodeComponentViews(IEnumerable<StructurizrJsonComponentView>? views) {
            if (views == null)
            {
                return Enumerable.Empty<ViewToImport>();
            }
            return this.mapper.Map<IEnumerable<ViewToImport>>(views);
        }

        public IEnumerable<ViewToImport> ExplodeDynamicViews(IEnumerable<StructurizrJsonDynamicView>? views) {
            if (views == null)
            {
                return Enumerable.Empty<ViewToImport>();
            }
            return this.mapper.Map<IEnumerable<ViewToImport>>(views);
        }

        public IEnumerable<ViewToImport> ExplodeDeploymentViews(IEnumerable<StructurizrJsonDeploymentView>? views) {
            if (views == null)
            {
                return Enumerable.Empty<ViewToImport>();
            }
            return this.mapper.Map<IEnumerable<ViewToImport>>(views);
        }

        public IEnumerable<ViewToImport> ExplodeFilteredViews(IEnumerable<StructurizrJsonFilteredView>? views) {
            if (views == null)
            {
                return Enumerable.Empty<ViewToImport>();
            }
            return this.mapper.Map<IEnumerable<ViewToImport>>(views);
        }

        public IEnumerable<ViewToImport> ExplodeImageViews(IEnumerable<StructurizrJsonImageView>? views) {
            if (views == null)
            {
                return Enumerable.Empty<ViewToImport>();
            }
            return this.mapper.Map<IEnumerable<ViewToImport>>(views);
        }

        public ViewConfigurationToImport ExplodeViewConfiguration(StructurizrJsonConfiguration? viewConfiguration)
        {
            return new ViewConfigurationToImport()
            {
                ElementStyles = this.mapper.Map<IEnumerable<ElementStyleToImport>>(viewConfiguration?.Styles?.Elements),
                RelationshipStyles = this.mapper.Map<IEnumerable<RelationshipStyleToImport>>(viewConfiguration?.Styles?.Relationships),
                Terminology = this.mapper.Map<TerminologyToImport>(viewConfiguration?.Terminology),
                Themes = viewConfiguration?.Themes,
            };
        }
    }
}
