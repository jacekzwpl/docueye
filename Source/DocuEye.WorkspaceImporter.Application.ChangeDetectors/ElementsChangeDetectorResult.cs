using DocuEye.ModelKeeper.Model;
using System.Collections.Generic;
using System.Linq;

namespace DocuEye.WorkspaceImporter.Application.ChangeDetectors
{
    public class ElementsChangeDetectorResult
    {
        public List<Element> ElementsToAdd { get; set; } = new List<Element>();
        public List<Element> ElementsToChange { get; set; } = new List<Element>();
        public List<Element> ElementsToDelete { get; set; } = new List<Element>();
    }
}
