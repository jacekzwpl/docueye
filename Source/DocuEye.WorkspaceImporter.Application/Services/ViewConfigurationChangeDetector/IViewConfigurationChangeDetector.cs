using DocuEye.Structurizr.Model;
using DocuEye.WorkspacesKeeper.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Application.Services.ViewConfigurationChangeDetector
{
    /// <summary>
    /// Service interface for detecting changes in view configuration
    /// </summary>
    public interface IViewConfigurationChangeDetector
    {
        /// <summary>
        /// Detects changes in view configuration
        /// </summary>
        /// <param name="workspaceId">The ID of workspace</param>
        /// <param name="workspaceData">Workspace data</param>
        /// <returns>Workspace view configuration that is ready to apply</returns>
        ViewConfiguration DetectViewConfigurationChanges(string workspaceId, StructurizrWorkspace workspaceData);
    }
}
