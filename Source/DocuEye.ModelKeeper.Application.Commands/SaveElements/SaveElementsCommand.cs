using DocuEye.ModelKeeper.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocuEye.ModelKeeper.Application.Commands.SaveElements
{
    /// <summary>
    /// Save elements command
    /// </summary>
    public class SaveElementsCommand : IRequest
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
