using DocuEye.Infrastructure.Mediator.Commands;
using DocuEye.ModelKeeper.Model;
using System.Collections.Generic;
using System.Linq;

namespace DocuEye.ModelKeeper.Application.Commands.SaveElements
{
    /// <summary>
    /// Save elements command
    /// </summary>
    public class SaveElementsCommand : ICommand
    {
        /// <summary>
        /// Elements that should be updated
        /// </summary>
        public IEnumerable<Element> ElementsToChange { get; set; } = Enumerable.Empty<Element>();
        /// <summary>
        /// Elements that should be created
        /// </summary>
        public IEnumerable<Element> ElementsToAdd { get; set; } = Enumerable.Empty<Element>();
        /// <summary>
        /// Elements that should be removed from the model
        /// </summary>
        public IEnumerable<Element> ElementsToDelete { get; set; } = Enumerable.Empty<Element>();

    }
}
