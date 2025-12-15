using DocuEye.Structurizr.Json.Model;
using DocuEye.Structurizr.Json.Model.Maps;
using DocuEye.WorkspaceImporter.Api.Model.ViewConfiguration;
using DocuEye.WorkspaceImporter.Api.Model.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DocuEye.Structurizr.Model.Exploders
{
    public class ViewsExploder
    {


        public ViewsExploder()
        {
        }


        public IEnumerable<ViewToImport> ExplodeSystemLandscapeViews(IEnumerable<StructurizrJsonSystemLandscapeView>? views) { 
            if(views == null)
            {
                return Enumerable.Empty<ViewToImport>();
            }
            return views.ToViewToImport();
        }

        public IEnumerable<ViewToImport> ExplodeSystemContextViews(IEnumerable<StructurizrJsonSystemContextView>? views) {
            if (views == null)
            {
                return Enumerable.Empty<ViewToImport>();
            }
            return views.ToViewToImport();
        }

        public IEnumerable<ViewToImport> ExplodeContainerViews(IEnumerable<StructurizrJsonContainerView>? views) {
            if (views == null)
            {
                return Enumerable.Empty<ViewToImport>();
            }
            return views.ToViewToImport();
        }

        public IEnumerable<ViewToImport> ExplodeComponentViews(IEnumerable<StructurizrJsonComponentView>? views) {
            if (views == null)
            {
                return Enumerable.Empty<ViewToImport>();
            }
            return views.ToViewToImport();
        }

        public IEnumerable<ViewToImport> ExplodeDynamicViews(IEnumerable<StructurizrJsonDynamicView>? views) {
            if (views == null)
            {
                return Enumerable.Empty<ViewToImport>();
            }
            return views.ToViewToImport();
        }

        public IEnumerable<ViewToImport> ExplodeDeploymentViews(IEnumerable<StructurizrJsonDeploymentView>? views) {
            if (views == null)
            {
                return Enumerable.Empty<ViewToImport>();
            }
            return views.ToViewToImport();
        }

        public IEnumerable<ViewToImport> ExplodeFilteredViews(IEnumerable<StructurizrJsonFilteredView>? views) {
            if (views == null)
            {
                return Enumerable.Empty<ViewToImport>();
            }
            return views.ToViewToImport();
        }

        public IEnumerable<ViewToImport> ExplodeImageViews(IEnumerable<StructurizrJsonImageView>? views) {
            if (views == null)
            {
                return Enumerable.Empty<ViewToImport>();
            }
            return views.ToViewToImport();
        }

        public ViewConfigurationToImport ExplodeViewConfiguration(StructurizrJsonConfiguration? viewConfiguration)
        {
            return new ViewConfigurationToImport()
            {
                ElementStyles = viewConfiguration?.Styles?.Elements?.ToElementStyleToImport() ?? Array.Empty<ElementStyleToImport>(),
                RelationshipStyles = viewConfiguration?.Styles?.Relationships?.ToRelationshipStyleToImport() ?? Array.Empty<RelationshipStyleToImport>(),
                Terminology = viewConfiguration?.Terminology?.ToTerminologyToImport(),
                Themes = viewConfiguration?.Themes,
            };
        }
    }
}
