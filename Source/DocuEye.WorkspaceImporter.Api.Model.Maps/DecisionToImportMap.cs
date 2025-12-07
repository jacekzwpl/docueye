using DocuEye.DocsKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Docs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Api.Model.Maps
{
    public static class DecisionToImportMap
    {
        public static Decision ToDecision(this DecisionToImport source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new Decision
            {
                DslId = source.StrucuturizrId ?? string.Empty,
                Title = source.Title ?? string.Empty,
                Status = source.Status ?? string.Empty,
                Content = source.Content ?? string.Empty,
                Format = source.Format ?? string.Empty,
                Date = string.IsNullOrWhiteSpace(source.Date) ? DateTime.Now : DateTime.Parse(source.Date),
                DocumentationId = source.DocumentationId
            };
        }
    }
}
