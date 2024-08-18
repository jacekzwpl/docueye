using DocuEye.WorkspaceImporter.Api.Model.Docs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Application.Commands.ImportDecision
{
    public class ImportDecisionCommand : IRequest<ImportDecisionResult>
    {
        public string WorkspaceId { get; set; } = null!;
        public string ImportKey { get; set; } = null!;

        public DecisionToImport Decision { get; set; } = null!;

        public ImportDecisionCommand(string workspaceId, string importKey, DecisionToImport decision)
        {
            WorkspaceId = workspaceId;
            ImportKey = importKey;
            Decision = decision;
        }
    }
}
