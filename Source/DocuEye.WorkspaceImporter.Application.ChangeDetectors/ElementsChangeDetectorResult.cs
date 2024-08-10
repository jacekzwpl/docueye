using DocuEye.ModelKeeper.Model;
using System.Collections.Generic;
using System.Linq;

namespace DocuEye.WorkspaceImporter.Application.ChangeDetectors
{
    public class ElementsChangeDetectorResult
    {
        public List<string> ElementsToAdd { get; set; } = new List<string>();
        public List<string> ElementsToChange { get; set; } = new List<string>();
        public List<string> ElementsToDelete { get; set; } = new List<string>();

        public void AddResult(ElementsChangeDetectorResult result)
        {
            ElementsToAdd.AddRange(result.ElementsToAdd);
            ElementsToChange.AddRange(result.ElementsToChange);
            ElementsToDelete.AddRange(result.ElementsToDelete);
        }
    }
}
