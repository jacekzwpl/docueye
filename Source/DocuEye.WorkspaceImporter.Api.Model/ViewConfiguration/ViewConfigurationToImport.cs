using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocuEye.WorkspaceImporter.Api.Model.ViewConfiguration
{
    public class ViewConfigurationToImport
    {
        public IEnumerable<ElementStyleToImport> ElementStyles { get; set; } = Enumerable.Empty<ElementStyleToImport>();
        public IEnumerable<RelationshipStyleToImport> RelationshipStyles { get; set; } = Enumerable.Empty<RelationshipStyleToImport>();
        public TerminologyToImport? Terminology { get; set; }
        public IEnumerable<string>? Themes { get; set; }
        public string? GroupSeparator { get; set; } = "|";
    }
}
