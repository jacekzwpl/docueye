using Microsoft.Extensions.DependencyInjection;

namespace DocuEye.WorkspacesKeeper.Application
{
    /// <summary>
    /// Hosting extensions for WorkspaceKeeper module
    /// </summary>
    public static class HostingExtensions
    {
        /// <summary>
        /// Configures services
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection ConfigureWorkspacesKeeperApplicationServices(this IServiceCollection services)
        {
            services.AddHttpClient("ThemesClient");
            return services;
        }
    }
}
