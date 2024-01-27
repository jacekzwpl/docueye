using DocuEye.DocsKeeper.Model;
using DocuEye.ModelKeeper.Model;
using System.Collections.Generic;

namespace DocuEye.WorkspaceImporter.Application.Services.WorkspaceChangeDetector
{
    /// <summary>
    /// Result of detecting elements changes 
    /// </summary>
    public class ElementsChangesResult
    {
        /// <summary>
        /// List of elements that should be updated
        /// </summary>
        public List<Element> ElementsToChange { get; set; } = new List<Element>();
        /// <summary>
        /// List of elements that should be created
        /// </summary>
        public List<Element> ElementsToAdd { get; set; } = new List<Element>();
        /// <summary>
        /// List of elements that should be removed form model
        /// </summary>
        public List<Element> ElementsToDelete { get; set; } = new List<Element>();
        /// <summary>
        /// Lsit of documentations that should be created 
        /// </summary>
        public List<Documentation> DocumentationsToAdd { get; set; } = new List<Documentation>();
        /// <summary>
        /// List of images that should be created
        /// </summary>
        public List<Image> ImagesToAdd { get; set; } = new List<Image>();
        /// <summary>
        /// List of decisions that should be created
        /// </summary>
        public List<Decision> DecisionsToAdd { get; set; } = new List<Decision>();
        /// <summary>
        /// Adds another result to this one
        /// </summary>
        /// <param name="result"></param>
        public void AddResult(ElementsChangesResult result)
        {
            ElementsToAdd.AddRange(result.ElementsToAdd);
            ElementsToChange.AddRange(result.ElementsToChange);
            ElementsToDelete.AddRange(result.ElementsToDelete);
            DocumentationsToAdd.AddRange(result.DocumentationsToAdd);
            ImagesToAdd.AddRange(result.ImagesToAdd);
            DecisionsToAdd.AddRange(result.DecisionsToAdd);
        }
    }
}
