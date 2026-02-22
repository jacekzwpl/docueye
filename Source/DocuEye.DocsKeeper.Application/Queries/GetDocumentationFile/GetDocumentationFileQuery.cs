using DocuEye.DocsKeeper.Model;
using DocuEye.Infrastructure.Mediator.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.DocsKeeper.Application.Queries.GetDocumentationFile
{
    public class GetDocumentationFileQuery : IQuery<DocumentationFile?>
    {
        public string WorkspaceId { get; set; } = null!;
        public string ElementId { get; set; } = null!;
        public string DocumentationType { get; set; } = null!;
    }
}
