using DocuEye.DocsKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Docs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Api.Model.Maps
{
    public static class DecisionLinkToImportMap
    {
        public static DecisionLink MapToDecisionLink(this DecisionLinkToImport source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new DecisionLink
            {
               Description = source.Description
            };
        }
    }
}
