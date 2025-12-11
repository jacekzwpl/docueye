using DocuEye.Infrastructure.Mediator.Commands;
using DocuEye.WorkspacesKeeper.Model;


namespace DocuEye.WorkspacesKeeper.Application.Commands.SaveViewConfiguration
{
    /// <summary>
    /// Saves workspace view configuration
    /// </summary>
    public class SaveViewConfigurationCommand : ICommand
    {
        /// <summary>
        /// View configuration
        /// </summary>
        public ViewConfiguration ViewConfiguration { get; set; } = null!;
    }
}
