using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Application.ChangeDetectors
{
    public class RelationshipsChangeDetectorResult
    {
        public List<string> RelationshipsToAdd { get; set; } = new List<string>();
        public List<string> RelationshipsToChange { get; set; } = new List<string>();
        public List<string> RelationshipsToDelete { get; set; } = new List<string>();
    }
}
