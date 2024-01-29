using AutoMapper;
using DocuEye.Structurizr.Model;
using DocuEye.ViewsKeeper.Model;
using System.Collections.Generic;

namespace DocuEye.WorkspaceImporter.Application.Services.ViewsExploder
{
    /// <summary>
    /// Service for exploding views from structurizr json
    /// </summary>
    public class ViewsExploderService : IViewsExploderService
    {
        private readonly IMapper mapper;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="mapper">Automapper service</param>
        public ViewsExploderService(IMapper mapper)
        {
            this.mapper = mapper;
        }
        /// <summary>
        /// Explodes views from structurizr json
        /// </summary>
        /// <param name="structurizrViews">Views object from structurizr json</param>
        /// <returns>Result of exploding views from structurizr json</returns>
        public ViewsExplodeResult Explode(StructurizrViews? structurizrViews)
        {
            if(structurizrViews == null)
            {
                return new ViewsExplodeResult();
            }
            var result = new ViewsExplodeResult();
            if(structurizrViews.SystemLandscapeViews != null)
            {
                foreach (var structurizrView in structurizrViews.SystemLandscapeViews)
                {
                    result.LandscapeViews.Add(this.mapper.Map<ExplodedSystemLandscapeView>(structurizrView));
                }
            }

            if (structurizrViews.SystemContextViews != null)
            {
                foreach (var structurizrView in structurizrViews.SystemContextViews)
                {
                    result.SystemContextViews.Add(this.mapper.Map<ExplodedSystemContextView>(structurizrView));
                }
            }

            if (structurizrViews.ContainerViews != null)
            {
                foreach (var structurizrView in structurizrViews.ContainerViews)
                {
                    result.ContainerViews.Add(this.mapper.Map<ExplodedContainerView>(structurizrView));
                }
            }

            if (structurizrViews.ComponentViews != null)
            {
                foreach (var structurizrView in structurizrViews.ComponentViews)
                {
                    result.ComponentViews.Add(this.mapper.Map<ExplodedComponentView>(structurizrView));
                }
            }

            if (structurizrViews.DynamicViews != null)
            {
                foreach (var structurizrView in structurizrViews.DynamicViews)
                {
                    result.DynamicViews.Add(this.mapper.Map<ExplodedDynamicView>(structurizrView));
                }
            }

            if (structurizrViews.DeploymentViews != null)
            {
                foreach (var structurizrView in structurizrViews.DeploymentViews)
                {
                    result.DeploymentViews.Add(this.mapper.Map<ExplodedDeploymentView>(structurizrView));
                }
            }

            if (structurizrViews.ImageViews != null)
            {
                foreach (var structurizrView in structurizrViews.ImageViews)
                {
                    result.ImagesViews.Add(this.mapper.Map<ExplodedImageView>(structurizrView));
                }
            }

            if (structurizrViews.FilteredViews != null)
            {
                result.FilteredViews = this.mapper.Map<List<FilteredView>>(structurizrViews.FilteredViews);
            }

            return result;
        }
    }
}
