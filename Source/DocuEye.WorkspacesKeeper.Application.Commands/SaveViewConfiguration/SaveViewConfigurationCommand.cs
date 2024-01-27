using DocuEye.WorkspacesKeeper.Model;
using MediatR;

namespace DocuEye.WorkspacesKeeper.Application.Commands.SaveViewConfiguration
{
    /// <summary>
    /// Saves workspace view configuration
    /// </summary>
    public class SaveViewConfigurationCommand : IRequest
    {
        /// <summary>
        /// View configuration
        /// </summary>
        public ViewConfiguration ViewConfiguration { get; set; } = null!;
    }
}
