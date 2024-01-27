using DocuEye.WorkspaceImporter.Application.Services.DecisionsExploder;
using DocuEye.WorkspaceImporter.Application.Services.ModelExploder;
using DocuEye.WorkspaceImporter.Application.Services.ViewConfigurationChangeDetector;
using DocuEye.WorkspaceImporter.Application.Services.ViewsExploder;
using DocuEye.WorkspaceImporter.Application.Services.WorkspaceChangeDetector;
using Microsoft.Extensions.DependencyInjection;

namespace DocuEye.WorkspaceImporter.Application
{
    /// <summary>
    /// Hosting extensions for WorkspaceImporter module
    /// </summary>
    public static class HostingExtensions
    {
        /// <summary>
        /// Configures services required by WorkspaceImporter module
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection ConfigureWorkspaceImporterApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IWorkspaceChangeDetectorService, WorkspaceChangeDetectorService>();
            services.AddTransient<IModelExploderService, ModelExploderService>();
            services.AddTransient<IViewsExploderService, ViewsExploderService>();
            services.AddTransient<IDecisionsExploderService, DecisionsExploderService>();
            services.AddTransient<IViewConfigurationChangeDetector, ViewConfigurationChangeDetector>();
            return services;
        }
    }
}
