using AutoMapper;
using DocuEye.Structurizr.Model;
using DocuEye.WorkspacesKeeper.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocuEye.WorkspaceImporter.Application.Services.ViewConfigurationChangeDetector
{
    /// <summary>
    /// Service for detecting changes in view configuration
    /// </summary>
    public class ViewConfigurationChangeDetector : IViewConfigurationChangeDetector
    {
        private readonly IMapper mapper;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="mapper">Automapper service</param>
        public ViewConfigurationChangeDetector(IMapper mapper) { 
            this.mapper = mapper;
        }
        /// <summary>
        /// Detects changes in view configuration
        /// </summary>
        /// <param name="workspaceId">The ID of workspace</param>
        /// <param name="workspaceData">Workspace data</param>
        /// <returns>Workspace view configuration that is ready to apply</returns>
        public ViewConfiguration DetectViewConfigurationChanges(string workspaceId, StructurizrWorkspace workspaceData)
        {
            ViewConfiguration viewConfiguration = new ViewConfiguration()
            {
                Id = workspaceId,
                GroupSeparator = workspaceData.Model == null ?
                    "|" : workspaceData.Model.GroupSeparator
            };
            if (workspaceData.Views?.Configuration != null)
            {
                if (workspaceData.Views.Configuration.Styles == null)
                {
                    viewConfiguration.ElementStyles = Enumerable.Empty<ElementStyle>();
                }
                else
                {
                    viewConfiguration = this.mapper.Map<StructurizrConfigurationStyles, ViewConfiguration>(workspaceData.Views.Configuration.Styles, viewConfiguration);
                }
                viewConfiguration.Themes = workspaceData.Views.Configuration.Themes;
                viewConfiguration.Terminology = this.mapper.Map<Terminology>(workspaceData.Views.Configuration.Terminology);
            }
            return viewConfiguration;
        }
    }
}
