﻿using DocuEye.WorkspaceImporter.Api.Model.Docs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Application.Commands.ImportDecisionsLinks
{
    public class ImportDecisionsLinksCommand : IRequest<ImportDecisionsLinksResult>
    {
        public string WorkspaceId { get; set; } = null!;

        public string ImportKey { get; set; } = null!;

        public string DocumentationId { get; set; } = null!;
        public string DecisionDslId { get; set; } = null!;


        public List<DecisionLinkToImport> DecisionLinks { get; set; } = null!;

        public ImportDecisionsLinksCommand(string workspaceId, string importKey, string documnetationId, string decisiondslId,  List<DecisionLinkToImport> decisionLinks)
        {
            WorkspaceId = workspaceId;
            ImportKey = importKey;
            DocumentationId = documnetationId;
            DecisionDslId = decisiondslId;
            DecisionLinks = decisionLinks;
        }
    }
}
